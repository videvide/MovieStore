using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Lib.Models
{
    public class Customer
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

        [MaxLength(255)]
        [DisplayName("Billing Address")]
        public string BillingAddress { get; set; }

        [MaxLength(100)]
        [DisplayName("Billing City")]
        public string BillingCity { get; set; }

        [DataType(DataType.PostalCode)]
        [DisplayName("Billing Zip Code")]
        public string BillingZip { get; set; }

        [MaxLength(255)]
        [DisplayName("Deliver Address")]
        public string DeliveryAddress { get; set; }

        [MaxLength(100)]
        [DisplayName("Delivery City")]
        public string DeliveryCity { get; set; }

        [DataType(DataType.PostalCode)]
        [DisplayName("Postal Zip Code")]
        public string DeliveryZip { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Phone]
        [DisplayName("Phone Number")]
        public string PhoneNo { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}