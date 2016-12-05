using System.Linq;
using BangazonUserAuth.Data;
using BangazonUserAuth.Models;
using Microsoft.AspNetCore.Identity;

namespace BangazonUserAuth.ViewModels
{   //Class Name: SingleProductViewModel
    //Author: Zack Repass
    //Purpose of the Class: This ViewModel is for giving data to the Single.cshtml view, allowing customers to see each product individually.
    //Methods in Class: None.
    
    public class SingleProductViewModel : BaseViewModel
    {
        public Product Product {get;set;} // This property gives the ViewModel access to the Product.cs model and its properties.

        public string CustomerName {get;set;} //Displayed on detail view to show sellers name.

        public SingleProductViewModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1) : base(userManager, ctx1) { } 
    }
}
