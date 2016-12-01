using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BangazonUserAuth.Data;
using BangazonUserAuth.Models;
using BangazonUserAuth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonUserAuth.Controllers
{
    //Class Name: ProductsController
    //Author: Debbie Bourne, Delaine Wendling
    //Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to products.
    //Methods in Class: Index(), Types(), TypesList(), Single(), New(), Overloaded New(Product product), AddToCart().
    public class ProductsController : Controller
    {
        //Bringing in the context from our DB and storing it in a local varialbe named BangazonUserAuthContext.
        private BangazonUserAuthContext context;

        public ProductsController (BangazonUserAuthContext ctx)
        {
            context = ctx;
        }

        //Storing our ActiveCustomer singleton in an private instance.
        private ActiveCustomer singleton = ActiveCustomer.Instance;

        //Method Name: Index
        //Purpose of the Method: This method is the first method that is loaded when a user visits Bangazon. This method will get all of the products in the database and show them on the homepage.
        //Arguments in Method: None.
        public async Task<IActionResult> Index()
        {
            AllProductsViewModel model = new AllProductsViewModel(context);

            model.Products = await context.Product.ToListAsync();
 
            return View(model);
        }

        //Method Name: Types
        //Purpose of the Method: This will load all of the ProductTypes from our DB and pass them to the view.
        //Arguments in Method: None.
        public async Task<IActionResult> Types()
        {
            AllProductTypesViewModel model = new AllProductTypesViewModel(context);
           
            model.ProductTypes = await context.ProductType.ToListAsync();

            return View(model);
        }

        //Method Name: TypesList
        //Purpose of the Method: This method returns all products that fall within a ProductType, using the ProductTypeId.
        //Arguments in Method: The ProductTypeId. 
        public async Task<IActionResult> TypesList([FromRoute] int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            AllSubProductTypesViewModel model = new AllSubProductTypesViewModel(context);

            model.SubProductTypes = await context.SubProductType.Where(p => p.ProductTypeId == id).ToListAsync();
            model.ProductType = await context.ProductType.Where(p => p.ProductTypeId == id).SingleOrDefaultAsync();
            
            if (model.SubProductTypes == null)
            {
                return NotFound();
            }
            return View(model);
        }

        //Method Name: ProductsInSubType
        //Purpose of the Method: This method gets all the products in a selected subtype and returns them.
        //Arguments in Method: Gets the subtypeId from the route.
        public async Task<IActionResult> ProductsInSubType([FromRoute] int id) 
        {
            AllProductsViewModel model = new AllProductsViewModel(context);

            model.Products =  await context.Product.Where(p => p.SubProductTypeId == id).ToListAsync();
            model.SubProductType = await context.SubProductType.Where(p => p.SubProductTypeId == id).SingleOrDefaultAsync();


            if (model.Products == null)
            {
                return NotFound();
            }

            return View(model);
        }

        //Method Name: Single
        //Purpose of the Method: This method gets a specific product and returns it to the view.
        //Arguments in Method: The ProductId of the product you wish to pass to the view.
        public async Task<IActionResult> Single([FromRoute] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            SingleProductViewModel model = new SingleProductViewModel(context);

            var SingleProduct = await context.Product.SingleOrDefaultAsync(p => p.ProductId == id);
            
            var SellingCustomer = context.Customer.Where(c => c.CustomerId == SingleProduct.CustomerId).Single();

            model.Product = SingleProduct;
            model.CustomerName = $"{SellingCustomer.FirstName} {SellingCustomer.LastName}";

            if (model.Product == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        //Method Name: New
        //Purpose of the Method: This method returns the New.cshtml file in the Products folder, which will contain a form to add a new product.
        //Arguments in Method: None.
        public IActionResult New()
        {
            ProductTypesListViewModel model = new ProductTypesListViewModel(context);
            return View(model);
        }

        //Method Name: Overloaded New
        //Purpose of the Method: This method takes information from the add product form and posts that information to the database, if it is valid. If the information is invalid, the user will be returned back to the form view. 
        //Arguments in Method: This method takes in an argument of type Product from the form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //Make sure error messages are present in the view if the view is returned to the customer.
            return BadRequest();
        }

         //Method Name: AddToCart
         //Purpose of the Method: When called, this method should add a product to the current active order. If there isnt a current active order a new one should be made with the active customer.
         //Arguments in Method: The ProductId of the product to add to the active order.
         //Something to note, the methods here must return a json response to the ajax call so the ajax call can redirect the window to index.
         [HttpPost]
         public async Task<IActionResult> AddToCart([FromRoute] int id)
         {
             //Get the active customer's order
             var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId).SingleOrDefaultAsync(); 

             // If no active order create one and add the product to it.
             if (activeOrder == null)
             {
                 var order = new Order();
                 order.IsCompleted = false;
                 order.CustomerId = singleton.Customer.CustomerId;
                 context.Add(order);
                 await context.SaveChangesAsync();
                 var newOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId).SingleOrDefaultAsync();
                 var lineItem = new LineItem();
                 lineItem.OrderId = newOrder.OrderId;
                 lineItem.ProductId = Convert.ToInt32(id);
                 context.Add(lineItem);
                 await context.SaveChangesAsync();
                 return Json(id);
             }
             else 
             // Add the Product to the existing active order.
             {
                 var lineItem = new LineItem();
                 lineItem.OrderId = activeOrder.OrderId;
                 lineItem.ProductId = Convert.ToInt32(id);
                 context.Add(lineItem);
                 await context.SaveChangesAsync();
                 return Json(id);
             }
         }

        //Method Name: GetSubTypes
        //Purpose of the Method: This method is called when the user changes the Product Type dropdown list to select a larger product category. The method grabs all subTypes within that Product Type and returns them in a Json format. 
        //Arguments in Method: Takes in an integer, which is equal to the ProductTypeId of the selected larger Product Category. 
        [HttpPost]
        public IActionResult GetSubTypes([FromRoute]int id)
        {
            //get sub categories with that product type on them
            var subTypes = context.SubProductType.Where(p => p.ProductTypeId == id).ToList();
            return Json(subTypes);
        }
        //Method Name: Error
        //Purpose of the Method: Default error message, return Error view.
        //Arguments in Method: None.
        public IActionResult Error()
        {
            return View();
        }
    }
}
