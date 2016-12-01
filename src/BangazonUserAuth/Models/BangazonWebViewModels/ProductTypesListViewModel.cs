using System.Linq;
using BangazonUserAuth.Data;
using BangazonUserAuth.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    public Product Product {get; set;}
    private BangazonUserAuthContext context;
    public ProductTypesListViewModel(BangazonUserAuthContext ctx) : base(ctx)
    { 
        context = ctx;
        this.ProductTypesList = context.ProductType
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
