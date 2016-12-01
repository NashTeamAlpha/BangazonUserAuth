using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonUserAuth.Models
{
    //Class Name: ProductType
    //Author: Jamie Duke and Delaine Wendling
    //Purpose of the class: This class serves as a model for our ProductType objects being transfered, manipulated, and saved between our Database, context, and Views.
    //Methods in Class: None.
  public class ProductType
  {
    [Key]
    public int ProductTypeId {get; set;}
    [Required]
    public string Name {get; set;}
    public ICollection<SubProductType> SubProductTypes; 
    }
}
