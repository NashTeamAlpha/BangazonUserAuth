using BangazonUserAuth.Models;
using BangazonUserAuth.Data;

namespace BangazonUserAuth.ViewModels
{
    //Class Name: NewCustomerViewModel
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to pass the type of Customer to the view for our new customer form to model from.
    //Methods in Class: None.
    public class NewCustomerViewModel : BaseViewModel
    {
        public Customer Customer { get; set; }

        public NewCustomerViewModel(BangazonWebContext ctx) : base(ctx) { } 
    }
}
