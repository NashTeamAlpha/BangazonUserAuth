using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonUserAuth.Models
{
    //Class Name: Product
    //Author: Jamie Duke and Delaine Wendling
    //Purpose of the class: This class serves as a model for our Product objects being transfered, manipulated, and saved between our Database, context, and Views.
    //Methods in Class: None.
  public class Product
  {
    [Key]
    public int ProductId { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(255)]
    public string Description { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public double Price { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated { get; set; }
    [Required]  // Added to ProductTypeId
    public int SubProductTypeId { get; set; }
    public SubProductType SubProductType { get; set; }
    public ICollection<LineItem> LineItems;
    [Required]
    public virtual ApplicationUser User { get; set; }

    }
}
