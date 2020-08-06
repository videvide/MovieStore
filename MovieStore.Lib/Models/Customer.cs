using System;
using System.Collections.Generic;
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
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string BillingAddress { get; set; }

        [MaxLength(100)]
        public string BillingCity { get; set; }

        [DataType(DataType.PostalCode)]
        public int BillingZip { get; set; }

        [MaxLength(255)]
        public string DeliveryAddress { get; set; }

        [MaxLength(100)]
        public string DeliveryCity { get; set; }

        [DataType(DataType.PostalCode)]
        public string DeliveryZip { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Phone]
        public string PhoneNo { get; set; }
    }
}