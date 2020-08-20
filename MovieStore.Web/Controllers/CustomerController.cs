using Microsoft.AspNet.Identity;
using MovieStore.Lib.Models;
using MovieStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using MovieStore.Web.DataAccess;

namespace MovieStore.Web.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly CustomerDataAccess _customerDataAccess = new CustomerDataAccess();

        // GET: Customer
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();

            var user = _context.Customers.FirstOrDefault(c => c.ApplicationUserId == userId);

            if (user != null)
            {
                return View(user);
            }
            else return RedirectToAction("Login", "Account");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingCity,BillingZip,DeliveryAddress,DeliveryCity,DeliveryZip,EmailAddress,PhoneNo,ApplicationUserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            else return View(customer);
        }

        public ActionResult Orders()
        {
            var userId = User.Identity.GetUserId();
            var customer = _customerDataAccess.GetCustomer(userId);
            var orders = _context.Orders.Where(o => o.CustomerId == (customer.Id)).ToList();
            orders.Reverse();
            return View(orders);
        }

        public ActionResult OrderDetails(int? id)
        {
            var userId = User.Identity.GetUserId();
            var customer = _customerDataAccess.GetCustomer(userId);
            if (customer == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("OrderDetails", "Customers", new { id }) });
            }
            if (id == null)
            {
                return RedirectToAction("Orders", "Customer");
            }

            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return RedirectToAction("Orders", "Customer");
            }
            else if (order.CustomerId != customer.Id && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Orders", "Customer");
            }
            List<OrderRows> orderRows = _context.OrderRows.Where(or => or.OrderId == order.Id).ToList();

            if (orderRows == null)
            {
                return RedirectToAction("Orders", "Customer");
            }
            List<string> checkedMovies = new List<string>();
            List<MovieCartPartialViewModel> model = new List<MovieCartPartialViewModel>();

            foreach (OrderRows orderRow in orderRows)
            {
                var movies = _context.Movies.Where(m => m.Id == orderRow.MovieId).ToList();
                var movie = _context.Movies.FirstOrDefault(m => m.Id == orderRow.MovieId);
                if (!checkedMovies.Contains(movie.ImdbID))
                {
                    int count = 0;
                    decimal totalPrice = 0;
                    foreach (OrderRows o in orderRows.Where(or => or.MovieId == orderRow.MovieId).ToList())
                    {
                        totalPrice += _context.Movies.FirstOrDefault(m => m.Id == o.MovieId).Price;
                        count++;
                    }
                    checkedMovies.Add(movie.ImdbID);
                    MovieCartPartialViewModel obj = new MovieCartPartialViewModel
                    {
                        Movie = movie,
                        Count = count,
                        TotalPrice = totalPrice
                    };
                    model.Add(obj);
                }
            }

            return View(model);
        }
    }
}