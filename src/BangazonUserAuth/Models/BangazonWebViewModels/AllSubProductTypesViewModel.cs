using System.Collections.Generic;
using BangazonUserAuth.Models;
using BangazonUserAuth.Data;

namespace BangazonUserAuth.ViewModels
{
    //Class Name: AllSubProductTypesViewModel
    //Author: Jamie Duke
    //Purpose of the class: The purpose of this class is to manage the property and method that will produce the data and functionality needed for the view of all of the sub product types
    //Methods in Class: None.
    public class AllSubProductTypesViewModel : BaseViewModel
    {
        public IEnumerable<SubProductType> SubProductTypes { get; set; }
        public ProductType ProductType { get; set; }

        public AllSubProductTypesViewModel(BangazonWebContext ctx) : base(ctx) { } 
    }
}