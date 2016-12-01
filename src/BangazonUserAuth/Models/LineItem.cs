using System.ComponentModel.DataAnnotations;

namespace BangazonUserAuth.Models
{
    //Class Name: LineItem
    //Author: Zack Repass and Grant Regnier
    //Purpose of the class: This class serves as a model for our LineItem objects being transfered, manipulated, and saved between our Database, context, and Views.
    //Methods in Class: None.    
    public class LineItem
    {
        [Key]
        public int LineItemId { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
