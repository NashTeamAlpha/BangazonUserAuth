using BangazonUserAuth.Models;
using BangazonUserAuth.Data;
using Microsoft.AspNetCore.Identity;

namespace BangazonUserAuth.ViewModels
{
    //Class Name: PaymentTypeViewModel
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to pass the type of PaymentType to the view for our payment form to model from.
    //Methods in Class: None.
    public class PaymentTypeViewModel : BaseViewModel
    {
        public PaymentType PaymentType { get; set; }

        public PaymentTypeViewModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1) : base(userManager, ctx1) { } 
    }
}
