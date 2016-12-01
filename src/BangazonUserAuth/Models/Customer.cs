using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BangazonUserAuth.Models
{
    //Class Name: Customer
    //Author: Chris Smalley And Debbie Bourne
    //Purpose of the class: This class serves as a model for our Customer objects being transfered, manipulated, and saved between our Database, context, and Views.
    //Methods in Class: None.
    public class Customer
    {
        [Key]
        public int CustomerId {get;set;}

        [Required]
        [StringLength(50)]
        public string FirstName {get;set;}

        [Required]
        [StringLength(50)]
        public string LastName {get;set;}

        public ICollection<PaymentType> PaymentTypes;

        public ICollection<Order> Orders;
    }
}
