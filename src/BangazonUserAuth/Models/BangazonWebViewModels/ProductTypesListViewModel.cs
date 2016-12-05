using System.Linq;
using BangazonUserAuth.Data;
using BangazonUserAuth.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace BangazonUserAuth.ViewModels
{
  //Class Name: Products
  //Author: Delaine Wendling
  //Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to products.
  //Methods in Class: None.
  public class ProductTypesListViewModel : BaseViewModel
  {
    public List<SelectListItem> ProductTypesList { get; set; }
    public List<SelectListItem> SubProductTypesList { get; set; }
    private ApplicationDbContext newContext { get; set; }
    public Product Product {get; set;}
    public ProductTypesListViewModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx1) : base(userManager, ctx1)
        {
            newContext = ctx1;
        this.ProductTypesList = newContext.ProductType
          .OrderBy(type => type.Name)
          .AsEnumerable()
          .Select(li => new SelectListItem{
            Text = $"{li.Name}",
            Value = li.ProductTypeId.ToString()
          }).ToList();
        this.ProductTypesList.Insert(0, new SelectListItem{
          Text = "Choose Product Category",
          Value = ""
        });

        this.SubProductTypesList = new List<SelectListItem>();

        this.SubProductTypesList.Insert(0, new SelectListItem{
          Text = "Choose a Product Category to See Sub-Categories",
          Value = ""
        });
    }
  }
}
