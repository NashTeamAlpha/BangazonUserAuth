using System.Collections.Generic;
using BangazonUserAuth.Models;
using BangazonUserAuth.Data;
using Microsoft.AspNetCore.Identity;

namespace BangazonUserAuth.ViewModels
{
    //Class Name: AllProductTypesViewModel
    //Author: Debbie Bourne
    //Purpose of the class: The purpose of this class is to manage the property and method that will produce the data and functionality needed for the view of all of the product types
    //Methods in Class: None.
    public class AllProductTypesViewModel : BaseViewModel
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }

        public AllProductTypesViewModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1) : base(userManager, ctx1) { } 
    }
}