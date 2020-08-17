using MovieStore.Lib.DataAccess;
using MovieStore.Lib.Models;
using MovieStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly OMDBDataAccess _movieAccess;
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _movieAccess = new OMDBDataAccess();
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.ToList();

            //var m = new List<Movie>();
            //var existingMovies = new List<string>();
            //var rnd = new Random();
            //var i = 0;
            //while (i < 12)
            //{
            //    var mov = movies[rnd.Next(0, movies.Count)];
            //    if (!existingMovies.Contains(mov.ImdbID))
            //    {
            //        m.Add(mov);
            //        existingMovies.Add(mov.ImdbID);
            //        i++;
            //    }
            //}

            var topFiveRecent = (from m in movies
                                 orderby m.ReleaseYear descending
                                 select m).Take(5).ToList();

            var topFiveOldest = (from m in movies
                                 orderby m.ReleaseYear ascending
                                 select m).Take(5).ToList();

            var topFiveCheapest = (from m in movies
                                   orderby m.Price ascending
                                   select m).Take(5).ToList();

            var topFivePopular = (from m in movies
                                  orderby m.Price ascending
                                  select m).Take(5).ToList();

            var model = new HomeViewModel
            {
                TopFiveRecent = topFiveRecent,
                TopFiveOldest = topFiveOldest,
                TopFiveCheapest = topFiveCheapest,
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This website was created in the summer of 2020 during our 1st group project at Lexicon, Linköping.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";

            return View();
        }

        public ActionResult Top100()
        {
            var movies = _context.Movies.Take(100).ToList();

            if (movies.Count < 100) return RedirectToAction("Index");

            return View(movies);
        }
    }
}