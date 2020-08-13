using MovieStore.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Models
{
    public class MovieCartPartialViewModel
    {
        public Movie Movie { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}