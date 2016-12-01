using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonUserAuth.Models
{
    //Class Name: SubProductType
    //Author: Delaine Wendling
    //Purpose of the class: This class serves as a model for our SubProductTypes objects being transfered, manipulated, and saved between our Database, context, and Views.
    //Methods in Class: None.
  public class SubProductType
  {
    [Key]
    public int SubProductTypeId {get; set;}
    [Required]
    [StringLength(100)]
    public string Name {get; set;}
    [Required]
    [StringLength(255)]
    public string Description {get; set;}
    [Required]
    public int ProductTypeId {get; set;}
    public ProductType ProductType;
    public ICollection<Product> Products; 
    }
}
