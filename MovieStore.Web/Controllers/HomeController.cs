using MovieStore.Lib.DataAccess;
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

        public HomeController()
        {
            _movieAccess = new OMDBDataAccess();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This website was created in the summer of 2020 during our 1st group project at Lexicon, Linköping.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}