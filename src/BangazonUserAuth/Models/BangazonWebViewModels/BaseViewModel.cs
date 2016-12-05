using System.Linq;
using BangazonUserAuth.Data;
using BangazonUserAuth.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BangazonUserAuth.ViewModels
{
  //Class Name: BaseViewModel
  //Author: Grant Regnier
  //Purpose of the class: The purpose of this class is to hold the ChossenCustomer property, give the ListOfCustomers list to the _layout.cshtml and manage the route on the customer name partial directly under the customer select dropdown.
  //Methods in Class: None.
  public class BaseViewModel
    {
    private ApplicationDbContext newContext;
    private BangazonWebContext context;
    private readonly UserManager<ApplicationUser> _userManager;

    public BaseViewModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1, BangazonWebContext ctx2)
    {
            _userManager = userManager;
            newContext = ctx1;
            context = ctx2;
     }

    public BaseViewModel() { }
    
        //private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        //public string ShoppingCartItems {
        //  get {
        //     var user = GetCurrentUserAsync();
        //     var ActiveCustomerId = user.CustomerId;
        //     Customer customer = context.Customer.Single(c => c.CustomerId == ActiveCustomerId)


        //    if (customer == null)
        //    {
        //      // Return null because there should not be a number next to the link if a customer has not been chosen.
        //      return "";
        //    }
        //    if (customer != null){
        //      //If there is a customer but the customer does not have an active order then the shopping cart should have 0 items in it.
        //       Order order = context.Order.Where(o => o.CustomerId == customer.CustomerId && o.IsCompleted == false).SingleOrDefault();
        //       if (order == null){
        //         return " (0)";
        //       }
        //       //If the user has an active order then the number of products in that order will be returned
        //       if (order != null){
        //        List<LineItem> lineItems = context.LineItem.Where(l => l.OrderId == order.OrderId).ToList();
        //        string shoppingCartCount = lineItems.Count.ToString();
        //        return " ("+shoppingCartCount+")";
        //       }
        //    }
        //    return "";
        //  }
        //}

    }
}

