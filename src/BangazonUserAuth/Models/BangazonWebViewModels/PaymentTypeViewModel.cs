using BangazonUserAuth.Models;
using BangazonUserAuth.Data;

namespace BangazonUserAuth.ViewModels
{
    //Class Name: PaymentTypeViewModel
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to pass the type of PaymentType to the view for our payment form to model from.
    //Methods in Class: None.
    public class PaymentTypeViewModel : BaseViewModel
    {
        public PaymentType PaymentType { get; set; }

        public PaymentTypeViewModel(BangazonUserAuthContext ctx) : base(ctx) { } 
    }
}
