using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MovieStore.Lib.DataAccess;
using MovieStore.Lib.Models;
using MovieStore.Web.Models;

namespace MovieStore.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly OMDBDataAccess _movieAccess = new OMDBDataAccess();

        // GET: Movies
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public async Task<ActionResult> DetailsImdb(string id)
        {
            var mov = db.Movies.FirstOrDefault(m => m.ImdbID == id);

            if (mov != null)
            {
                return RedirectToAction("Details", new { id = mov.Id });
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MovieResponse movieRes = await _movieAccess.GetMovieById(id);
                if (movieRes == null)
                {
                    return HttpNotFound();
                }

                Movie movie = new Movie
                {
                    Director = movieRes.Director,
                    Genre = movieRes.Genre,
                    ImdbID = movieRes.imdbID,
                    ImdbRating = movieRes.imdbRating,
                    Plot = movieRes.Plot,
                    Poster = _movieAccess.FixPosterURL(movieRes.Poster),
                    Price = 199,
                    Rated = movieRes.Rated,
                    ReleaseYear = movieRes.Year,
                    Title = movieRes.Title
                };

                db.Movies.Add(movie);
                await db.SaveChangesAsync();

                var mo = db.Movies.FirstOrDefault(m => m.ImdbID == id);

                if (mo != null)
                {
                    return RedirectToAction("Details", new { id = mo.Id });
                }
                else
                {
                    return View(movie);
                }
            }
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Title,Director,ReleaseYear,Price,Rated,Genre,Plot,Poster,ImdbRating,ImdbID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Director,ReleaseYear,Price,Rated,Genre,Plot,Poster,ImdbRating,ImdbID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(string query)
        {
            var movieSearch = await _movieAccess.SearchMovies(query);
            return View(movieSearch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}