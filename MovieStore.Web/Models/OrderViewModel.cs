using MovieStore.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Models
{
    public class OrderViewModel
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }

        public List<Movie> Movies { get; set; }
    }
}