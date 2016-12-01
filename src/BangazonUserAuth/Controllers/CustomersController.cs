using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonUserAuth.Models;
using BangazonUserAuth.Data;
using BangazonUserAuth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonUserAuth.Controllers
{
    // Class Name: CustomersController
    // Authors: Chris Smalley, Zack Repass
    // Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to customers.
    // Methods in Class: New(), Overloaded New(Customer customer) ShoppingCart(), Payment(), OrderCompleted().
    public class CustomersController : Controller
    {
        //Bringing in the context from our DB and storing it in a local variable named BangazonUserAuthContext.
        private BangazonWebContext context;
        public CustomersController(BangazonWebContext ctx)
        {
            context = ctx;
        }

        //Storing our ActiveCustomer singleton in an private instance.
        private ActiveCustomer singleton = ActiveCustomer.Instance;

        //Method Name: New
        //Purpose of the Method: Loads new customer form to the view, view wil be static.
        //Arguments in Method: None.
        [HttpGet]
        public IActionResult New()
        {
            NewCustomerViewModel model = new NewCustomerViewModel(context);
            return View(model);
        }

        //Method Name: Overloaded New
        //Purpose of the Method: Will take an optional parameter of the type "customer." Takes form data then checks its validity. Post to customer table in database then redirects to home page.
        //Arguments in Method: A new customer object taken from the form of Customer/New.cshtml.
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> New(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Add(customer);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Products");
            }
            return BadRequest();
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

            var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId).SingleOrDefaultAsync();
            Console.WriteLine(activeOrder);

            ShoppingCartViewModel model = new ShoppingCartViewModel(context);

            if (activeOrder == null)
            {
               var product = new Product(){Description="You have no products in your cart!", Name=""};
                model.Products = new List<Product>();
                model.Products.Add(product);
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

            model.CartTotal = CartTotal;
            model.Products = ListOfProducts;

            return View(model);
            
        }

        //Method Name: Payment
        //Purpose of the Method: Method should take you to the Payment view with form to add Payment.
        //Arguments in Method: None.
         [HttpGet]
         public IActionResult Payment()
        {

            PaymentTypeViewModel model = new PaymentTypeViewModel(context);

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

            BaseViewModel model = new BaseViewModel(context);

            return View(model);
        }
        //Method Name: SubmitOrder
        //Purpose of the Method: To change the isCompleted bool from false to true on the active order for this customer and direct the user to the OrderCompleted view.
        //Arguments in Method: None.
        [HttpPatch]
        public async Task<IActionResult> SubmitOrder([FromRoute]int id)
        {
            var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId)
            .SingleOrDefaultAsync();
            activeOrder.PaymentType = context.PaymentType.Where(pt => pt.PaymentTypeId == id).SingleOrDefault();
            activeOrder.DateCompleted = DateTime.Today;
            activeOrder.IsCompleted = true;
            context.Update(activeOrder);
            await context.SaveChangesAsync();

            return Json(id);
        }
        //Method Name: Activate
        //Purpose of the Method: To change the current ActiveCustomer singleton to whatever is selected in the top right dropdown select option.
        //Arguments in Method: Takes a CustomerId from the drop down select option in the navigation bar on change of selected option. 
        [HttpPost]
        public IActionResult Activate([FromRoute]int id)
        {
            // Find the corresponding customer in the DB
            var customer = context.Customer.SingleOrDefault(c => c.CustomerId == id);

            // Return 404 if not found
            if (customer == null)
            {
                return NotFound();
            }

            // Set the active customer to the selected on
            ActiveCustomer.Instance.Customer = customer;

            return Json(customer);
        }
    }
}
