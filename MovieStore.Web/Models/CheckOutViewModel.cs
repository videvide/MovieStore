using MovieStore.Lib.Models;
using System;
using System.Collections.Generic;
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
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(255)]
        public string BillingAddress { get; set; }
        [Required]
        [MaxLength(100)]
        public string BillingCity { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public string BillingZip { get; set; }
        [Required]
        [MaxLength(255)]
        public string DeliveryAddress { get; set; }
        [Required]
        [MaxLength(100)]
        public string DeliveryCity { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public string DeliveryZip { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [Phone]
        public string PhoneNo { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}