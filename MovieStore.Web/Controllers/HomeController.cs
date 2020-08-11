using MovieStore.Lib.DataAccess;
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

            ViewBag.Movies = movies.Take(10).ToList();

            return View(movies.Take(10).ToList());
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