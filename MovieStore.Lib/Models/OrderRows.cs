using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Lib.Models
{
    public class OrderRows
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }


    }
}
