using System.Collections.Generic;
using BangazonUserAuth.Models;
using BangazonUserAuth.Data;

namespace BangazonUserAuth.ViewModels
{
  //Class Name: AllProductsViewModel
  //Author: Debbie Bourne 
  //Purpose of the class: The purpose of this class is to hold and pass data data to the whichever view this ViewModel is passed. 
  //Methods in Class: None.
  public class AllProductsViewModel : BaseViewModel
  {
    public IEnumerable<Product> Products { get; set; }
    public SubProductType SubProductType { get; set; }
    public AllProductsViewModel(BangazonWebContext ctx) : base(ctx) { }
  }
}