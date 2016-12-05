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
using Microsoft.AspNetCore.Authorization;

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
        public CustomersController(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1)
        {
            _userManager = userManager;
            newContext = ctx1;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        //Method Name: ShoppingCart
        //Purpose of the Method: 
            //Gets all LineItems on active order and give data to the returned View. 
            //Gets all PaymentTypes of selected Customer and give data to the returned View.
            //This method returns the Customer/ShoppingCart view.
        //Arguments in Method: None.
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShoppingCart()
        {
            var user = await GetCurrentUserAsync();
            var activeOrder = await newContext.Order.Where(o => o.IsCompleted == false && o.User==user).SingleOrDefaultAsync();

            ShoppingCartViewModel model = new ShoppingCartViewModel(_userManager, newContext);

            if (activeOrder == null)
            {
               var product = new Product(){Description="You have no products in your cart!", Name=""};
                model.Products = new List<Product>();
                model.Products.Add(product);
                model.ListOfPaymentTypes = newContext.PaymentType
               .Where(pt => pt.User == user)
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

            List<LineItem> LineItemsOnActiveOrder = newContext.LineItem.Where(li => li.OrderId == activeOrder.OrderId).ToList();
            
            List<Product> ListOfProducts = new List<Product>();

            double CartTotal = 0;

            for(var i = 0; i < LineItemsOnActiveOrder.Count(); i++)
            {
                ListOfProducts.Add(newContext.Product.Where(p => p.ProductId == LineItemsOnActiveOrder[i].ProductId).SingleOrDefault());
                CartTotal += newContext.Product.Where(p => p.ProductId == LineItemsOnActiveOrder[i].ProductId).SingleOrDefault().Price;
            }

            model.ListOfPaymentTypes = newContext.PaymentType
               .Where(pt => pt.User == user)
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
         [Authorize]
         public async Task<IActionResult> Payment()
        {
            var user = await GetCurrentUserAsync();

            PaymentTypeViewModel model = new PaymentTypeViewModel(_userManager, newContext);

            return View(model);
        }

        //Method Name: Overloaded Payment
        //Purpose of the Method: This is the Overloaded method that actually adds the payments to the Db.
        //Arguments in Method: Takes a new PaymentType object from the form provided and posts it to the database.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Payment([FromForm]PaymentType paymentType)
        {
            ModelState.Remove("paymentType.User");

            if (ModelState.IsValid)
            {
                /*
                    If all other properties validation, then grab the 
                    currently authenticated user and assign it to the 
                    product before adding it to the db context
                */
                var user = await GetCurrentUserAsync();
                paymentType.User = user;

                newContext.Add(paymentType);

                await newContext.SaveChangesAsync();
                return RedirectToAction("ShoppingCart");
            }
            
            return BadRequest();
        }

        //Method Name: OrderCompleted
        //Purpose of the Method: To change the isCompleted bool from false to true on the active order for this customer and direct the user to the OrderCompleted view.
        //Arguments in Method: None.
        [HttpGet]
        [Authorize]
        public IActionResult OrderCompleted()
        {

            BaseViewModel model = new BaseViewModel(_userManager, newContext);

            return View(model);
        }
        //Method Name: SubmitOrder
        //Purpose of the Method: To change the isCompleted bool from false to true on the active order for this customer and direct the user to the OrderCompleted view.
        //Arguments in Method: None.
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> SubmitOrder([FromRoute]int id)
        {
            if (id == 0)
            {
                return NotFound(); 
            }
            var user = await GetCurrentUserAsync();
            var activeOrder = await newContext.Order.Where(o => o.IsCompleted == false && o.User==user)
            .SingleOrDefaultAsync();
            activeOrder.PaymentType = newContext.PaymentType.Where(pt => pt.PaymentTypeId == id).SingleOrDefault();
            activeOrder.DateCompleted = DateTime.Today;
            activeOrder.IsCompleted = true;
            newContext.Update(activeOrder);
            await newContext.SaveChangesAsync();

            return Json(id);
        }
    }
}
