using System.Linq;
using BangazonUserAuth.Data;
using BangazonUserAuth.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonUserAuth.ViewModels
{
  //Class Name: BaseViewModel
  //Author: Grant Regnier
  //Purpose of the class: The purpose of this class is to hold the ChossenCustomer property, give the ListOfCustomers list to the _layout.cshtml and manage the route on the customer name partial directly under the customer select dropdown.
  //Methods in Class: None.
  public class BaseViewModel
  {
    public List<SelectListItem> ListOfCustomers { get; set; }
    private BangazonWebContext context;
    private ActiveCustomer singleton = ActiveCustomer.Instance;
    public string Route {
      get {
        Customer customer = singleton.Customer;
        // If no customer has been chosen yet, it's value will be null
        if (customer == null)
        {
          // Return New route
          return "New";
        }
        //Return Shopping Cart Route
        return "ShoppingCart";
      }
    }
    public string ShoppingCartItems {
      get {
        Customer customer = singleton.Customer;

        if (customer == null)
        {
          // Return null because there should not be a number next to the link if a customer has not been chosen.
          return "";
        }
        if (customer != null){
          //If there is a customer but the customer does not have an active order then the shopping cart should have 0 items in it.
           Order order = context.Order.Where(o => o.CustomerId == customer.CustomerId && o.IsCompleted == false).SingleOrDefault();
           if (order == null){
             return " (0)";
           }
           //If the user has an active order then the number of products in that order will be returned
           if (order != null){
            List<LineItem> lineItems = context.LineItem.Where(l => l.OrderId == order.OrderId).ToList();
            string shoppingCartCount = lineItems.Count.ToString();
            return " ("+shoppingCartCount+")";
           }
        }
        return "";
      }
    }
    public Customer ChosenCustomer 
    {
      get
      {
        // Get the current value of the customer property of our singleton
        Customer customer = singleton.Customer;
        // If no customer has been chosen yet, it's value will be null
        if (customer == null)
        {

          // Return fake customer for now
          return new Customer () {
            FirstName = "Create",
            LastName = "Account"
          };
        }
        // If there is a customer chosen, return it
        return customer;
      }
      set
      {
        if (value != null)
        {
          singleton.Customer = value;
        }
      }
    }
    public BaseViewModel(BangazonWebContext ctx)
    {
        context = ctx;
        this.ListOfCustomers = context.Customer
            .OrderBy(l => l.LastName)
            .AsEnumerable()
            .Select(li => new SelectListItem { 
                Text = $"{li.FirstName} {li.LastName}",
                Value = li.CustomerId.ToString()
            }).ToList();
        
        this.ListOfCustomers.Insert(0, new SelectListItem {
          Text="Choose Customer"
        });
    }
    public BaseViewModel() { }
  }
}

