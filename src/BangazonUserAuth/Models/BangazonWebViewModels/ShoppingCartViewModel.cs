using System.Collections.Generic;
using BangazonUserAuth.Models;
using BangazonUserAuth.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BangazonUserAuth.ViewModels
{
    //Class Name: ShoppingCartViewModel
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to pass data from the controller to the shoppingcart view with a selectlist of PaymentTypes
    //Methods in Class: None.
  public class ShoppingCartViewModel : BaseViewModel
  {
    public List<SelectListItem> ListOfPaymentTypes { get; set; }
    public List<Product> Products { get; set; }
    public double CartTotal {get; set;}
    public int ActiveCustomerId { get; set; }
    private ApplicationDbContext newContext;
    private BangazonWebContext context;
    private readonly UserManager<ApplicationUser> _userManager;
    
        //Method Name: ShoppingCartViewModel
        //Purpose of the Method: Upon construction this should take the context and send a list of select items of the type PaymentType to the View. They should be the paymentTypes of the active customer.
        //Arguments in Method: BangazonUserAuthContext
    public ShoppingCartViewModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1, BangazonWebContext ctx2) : base(userManager, ctx1, ctx2)
    {
            _userManager = userManager;
            newContext = ctx1;
            context = ctx2;
            this.ListOfPaymentTypes = context.PaymentType
                .Where(pt => pt.CustomerId == ActiveCustomerId)
                .AsEnumerable()
                .Select(pt => new SelectListItem
                {
                    Text = $"{pt.FirstName} {pt.LastName} {pt.Processor} {pt.ExpirationDate}",
                    Value = pt.PaymentTypeId.ToString()
                }).ToList();

            this.ListOfPaymentTypes.Insert(0, new SelectListItem
            {
                Text = "Choose Payment Type"
            });
        }
    }
}