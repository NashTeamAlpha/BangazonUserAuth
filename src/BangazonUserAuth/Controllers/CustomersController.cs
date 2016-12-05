using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonUserAuth.Models;
using BangazonUserAuth.Data;
using BangazonUserAuth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonUserAuth.Controllers
{
    // Class Name: CustomersController
    // Authors: Chris Smalley, Zack Repass
    // Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to customers.
    // Methods in Class: New(), Overloaded New(Customer customer) ShoppingCart(), Payment(), OrderCompleted().
    public class CustomersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationDbContext newContext;
        private BangazonWebContext context;
        public CustomersController(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1, BangazonWebContext ctx2)
        {
            _userManager = userManager;
            newContext = ctx1;
            context = ctx2;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        //Method Name: Overloaded New
        //Purpose of the Method: Will take an optional parameter of the type "customer." Takes form data then checks its validity. Post to customer table in database then redirects to home page.
        //Arguments in Method: A new customer object taken from the form of Customer/New.cshtml.
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public int New(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Add(customer);
                context.SaveChangesAsync();
                Customer activeCustomer = context.Customer.Single(c => c.FirstName == customer.FirstName && c.LastName == customer.LastName);
                if (activeCustomer != null)
                {
                   return activeCustomer.CustomerId;
                       
                } else
                {
                    return 0;
                }
            }
            return 0;
        }


        //Method Name: ShoppingCart
        //Purpose of the Method: 
            //Gets all LineItems on active order and give data to the returned View. 
            //Gets all PaymentTypes of selected Customer and give data to the returned View.
            //This method returns the Customer/ShoppingCart view.
        //Arguments in Method: None.
        [HttpGet]
        public async Task<IActionResult> ShoppingCart()
        {
            var user = await GetCurrentUserAsync();
            var CustomerId = user.CustomerId;
            var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==CustomerId).SingleOrDefaultAsync();

            ShoppingCartViewModel model = new ShoppingCartViewModel(_userManager, newContext, context);

            if (activeOrder == null)
            {
               var product = new Product(){Description="You have no products in your cart!", Name=""};
                model.Products = new List<Product>();
                model.Products.Add(product);
                model.ActiveCustomerId = CustomerId;
                model.ListOfPaymentTypes = context.PaymentType
               .Where(pt => pt.CustomerId == CustomerId)
               .AsEnumerable()
               .Select(pt => new SelectListItem
               {
                   Text = $"{pt.FirstName} {pt.LastName} {pt.Processor} {pt.ExpirationDate}",
                   Value = pt.PaymentTypeId.ToString()
               }).ToList();

                model.ListOfPaymentTypes.Insert(0, new SelectListItem

                {
                    Text = "Choose Payment Type"
                });
                return View(model);
            }

            List<LineItem> LineItemsOnActiveOrder = context.LineItem.Where(li => li.OrderId == activeOrder.OrderId).ToList();
            
            List<Product> ListOfProducts = new List<Product>();

            double CartTotal = 0;

            for(var i = 0; i < LineItemsOnActiveOrder.Count(); i++)
            {
                ListOfProducts.Add(context.Product.Where(p => p.ProductId == LineItemsOnActiveOrder[i].ProductId).SingleOrDefault());
                CartTotal += context.Product.Where(p => p.ProductId == LineItemsOnActiveOrder[i].ProductId).SingleOrDefault().Price;
            }

            model.ListOfPaymentTypes = context.PaymentType
               .Where(pt => pt.CustomerId == CustomerId)
               .AsEnumerable()
               .Select(pt => new SelectListItem
               {
                   Text = $"{pt.FirstName} {pt.LastName} {pt.Processor} {pt.ExpirationDate}",
                   Value = pt.PaymentTypeId.ToString()
               }).ToList();

            model.ListOfPaymentTypes.Insert(0, new SelectListItem

            {
                Text = "Choose Payment Type"
            });
            model.CartTotal = CartTotal;
            model.Products = ListOfProducts;

            return View(model);
            
        }

        //Method Name: Payment
        //Purpose of the Method: Method should take you to the Payment view with form to add Payment.
        //Arguments in Method: None.
         [HttpGet]
         public async Task<IActionResult> Payment()
        {
            var user = await GetCurrentUserAsync();

            PaymentTypeViewModel model = new PaymentTypeViewModel(_userManager, newContext, context);
            model.ActiveCustomerId = user.CustomerId;

            return View(model);
        }

        //Method Name: Overloaded Payment
        //Purpose of the Method: This is the Overloaded method that actually adds the payments to the Db.
        //Arguments in Method: Takes a new PaymentType object from the form provided and posts it to the database.
        [HttpPost]
        public async Task<IActionResult> Payment([FromForm]PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                context.Add(paymentType);
                await context.SaveChangesAsync();
                return RedirectToAction("ShoppingCart");
            }
            return BadRequest();
        }

        //Method Name: OrderCompleted
        //Purpose of the Method: To change the isCompleted bool from false to true on the active order for this customer and direct the user to the OrderCompleted view.
        //Arguments in Method: None.
        [HttpGet]
        public IActionResult OrderCompleted()
        {

            BaseViewModel model = new BaseViewModel(_userManager, newContext, context);

            return View(model);
        }
        //Method Name: SubmitOrder
        //Purpose of the Method: To change the isCompleted bool from false to true on the active order for this customer and direct the user to the OrderCompleted view.
        //Arguments in Method: None.
        [HttpPatch]
        public async Task<IActionResult> SubmitOrder([FromRoute]int id)
        {
            var user = await GetCurrentUserAsync();
            var CustomerId = user.CustomerId;
            var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==CustomerId)
            .SingleOrDefaultAsync();
            activeOrder.PaymentType = context.PaymentType.Where(pt => pt.PaymentTypeId == id).SingleOrDefault();
            activeOrder.DateCompleted = DateTime.Today;
            activeOrder.IsCompleted = true;
            context.Update(activeOrder);
            await context.SaveChangesAsync();

            return Json(id);
        }
    }
}
