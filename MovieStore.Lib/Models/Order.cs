using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Lib.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
