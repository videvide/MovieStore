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

            var m = new List<Movie>();
            var existingMovies = new List<string>();
            var rnd = new Random();
            var i = 0;
            while (i < 12)
            {
                var mov = movies[rnd.Next(0, movies.Count)];
                if (!existingMovies.Contains(mov.ImdbID))
                {
                    m.Add(mov);
                    existingMovies.Add(mov.ImdbID);
                    i++;
                }
            }

            ViewBag.Movies = m;

            return View(m);
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
    }
}