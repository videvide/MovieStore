using MovieStore.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Models
{
    public class HomeViewModel
    {
        public List<Movie> TopFivePopular { get; set; }
        public List<Movie> TopFiveRecent { get; set; }
        public List<Movie> TopFiveOldest { get; set; }
        public List<Movie> TopFiveCheapest { get; set; }
    }
}