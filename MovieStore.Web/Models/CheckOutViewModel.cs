using MovieStore.Lib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Models
{
    public class CheckOutViewModel
    {
        public CustomerDetailsCheckOutViewModel Customer { get; set; }

        public List<Movie> Movies { get; set; }
    }

    public class CustomerDetailsCheckOutViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Billing Address")]
        public string BillingAddress { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Billing City")]
        public string BillingCity { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [DisplayName("Billing Zip Code")]
        public string BillingZip { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Delivery City")]
        public string DeliveryCity { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [DisplayName("Delivery Zip Code")]
        public string DeliveryZip { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string PhoneNo { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}