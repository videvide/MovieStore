using MovieStore.Lib.Models;
using MovieStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Cart
        public ActionResult Index()
        {
            var movies = Session["CartMovies"];

            return View(movies);
        }

        public ActionResult Remove(int movieId)
        {
            var sessionStorage = (List<Movie>)Session["CartMovies"];

            if (sessionStorage != null && sessionStorage.Count > 0)
            {
                Movie movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);
                sessionStorage.Remove(sessionStorage.LastOrDefault(m => m.Id == movie.Id));

                if (sessionStorage.Count < 1)
                {
                    sessionStorage = null;
                }

                Session["CartMovies"] = sessionStorage;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Add(int movieId)
        {
            Movie movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);
            int count = 0;

            if (movie != null)
            {
                var sessionStorage = (List<Movie>)Session["CartMovies"];

                if (sessionStorage != null && sessionStorage.Count > 0)
                {
                    sessionStorage.Add(movie);
                    count = sessionStorage.Count;
                    Session["CartMovies"] = sessionStorage;
                }
                else
                {
                    sessionStorage = new List<Movie>
                    {
                        movie
                    };
                    count = sessionStorage.Count;
                    Session["CartMovies"] = sessionStorage;
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveAll()
        {
            Session["CartMovies"] = null;

            return RedirectToAction("Index");
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int AddToCart(int movieId)
        {
            Movie movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);
            int count = 0;

            if (movie != null)
            {
                var sessionStorage = (List<Movie>)Session["CartMovies"];

                if (sessionStorage != null && sessionStorage.Count > 0)
                {
                    sessionStorage.Add(movie);
                    count = sessionStorage.Count;
                    Session["CartMovies"] = sessionStorage;
                }
                else
                {
                    sessionStorage = new List<Movie>
                    {
                        movie
                    };
                    count = sessionStorage.Count;
                    Session["CartMovies"] = sessionStorage;
                }
            }

            return count;
        }
    }
}