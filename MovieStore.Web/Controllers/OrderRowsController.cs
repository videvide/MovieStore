using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStore.Lib.Models;
using MovieStore.Web.Models;

namespace MovieStore.Web.Controllers
{
    public class OrderRowsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderRows
        public ActionResult Index()
        {
            var orderRows = db.OrderRows.Include(o => o.Movie).Include(o => o.Order);
            return View(orderRows.ToList());
        }

        // GET: OrderRows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderRows orderRows = db.OrderRows.Find(id);
            if (orderRows == null)
            {
                return HttpNotFound();
            }
            return View(orderRows);
        }

        // GET: OrderRows/Create
        public ActionResult Create()
        {
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title");
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id");
            return View();
        }

        // POST: OrderRows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,MovieId,Price")] OrderRows orderRows)
        {
            if (ModelState.IsValid)
            {
                db.OrderRows.Add(orderRows);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", orderRows.MovieId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderRows.OrderId);
            return View(orderRows);
        }

        // GET: OrderRows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderRows orderRows = db.OrderRows.Find(id);
            if (orderRows == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", orderRows.MovieId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderRows.OrderId);
            return View(orderRows);
        }

        // POST: OrderRows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,MovieId,Price")] OrderRows orderRows)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderRows).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", orderRows.MovieId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderRows.OrderId);
            return View(orderRows);
        }

        // GET: OrderRows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderRows orderRows = db.OrderRows.Find(id);
            if (orderRows == null)
            {
                return HttpNotFound();
            }
            return View(orderRows);
        }

        // POST: OrderRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderRows orderRows = db.OrderRows.Find(id);
            db.OrderRows.Remove(orderRows);
            db.SaveChanges();
            return RedirectToAction("Index");
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
