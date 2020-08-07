using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Lib.Models
{
    public class MovieUpdateHistory
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime LastUpdate { get; set; }
    }
}
