using Microsoft.AspNet.Identity;
using MovieStore.Lib.Models;
using MovieStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MovieStore.Web.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: CheckOut
        [HttpGet]
        public ActionResult Index()
        {
            // metod för att visa ordern och be om uppgifter för att slutföra köp
            if (User.Identity.IsAuthenticated)
            {
                List<Movie> movies = (List<Movie>)Session["CartMovies"];
                if (movies == null) return RedirectToAction("Index", "Home");
                string userId = User.Identity.GetUserId();
                Customer customer = _context.Customers.FirstOrDefault(cus => cus.ApplicationUserId == userId);
                CustomerDetailsCheckOutViewModel c = new CustomerDetailsCheckOutViewModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    BillingAddress = customer.BillingAddress,
                    BillingCity = customer.BillingCity,
                    BillingZip = customer.BillingZip,
                    DeliveryAddress = customer.DeliveryAddress,
                    DeliveryCity = customer.DeliveryCity,
                    DeliveryZip = customer.DeliveryZip,
                    EmailAddress = customer.EmailAddress,
                    PhoneNo = customer.PhoneNo,
                    ApplicationUserId = customer.ApplicationUserId
                };

                CheckOutViewModel model = new CheckOutViewModel
                {
                    Movies = movies,
                    Customer = c
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "CheckOut") });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingCity,BillingZip,DeliveryAddress,DeliveryCity,DeliveryZip,EmailAddress,PhoneNo,ApplicationUserId")] CustomerDetailsCheckOutViewModel customer)
        {
            if (ModelState.IsValid)
            {
                Customer c = new Customer
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    BillingAddress = customer.BillingAddress,
                    BillingCity = customer.BillingCity,
                    BillingZip = customer.BillingZip,
                    DeliveryAddress = customer.DeliveryAddress,
                    DeliveryCity = customer.DeliveryCity,
                    DeliveryZip = customer.DeliveryZip,
                    EmailAddress = customer.EmailAddress,
                    PhoneNo = customer.PhoneNo,
                    ApplicationUserId = customer.ApplicationUserId
                };

                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
                Order order = new Order
                {
                    OrderDate = DateTime.Now,
                    CustomerId = customer.Id,
                };
                OrderViewModel orderViewModel = new OrderViewModel
                {
                    Customer = c,
                    Order = order,
                    Movies = (List<Movie>)Session["CartMovies"]
                };
                TempData["orderViewModel"] = orderViewModel;
                return RedirectToAction("Payment");
            }

            List<Movie> movies = (List<Movie>)Session["CartMovies"];
            string userId = User.Identity.GetUserId();
            Customer cust = _context.Customers.FirstOrDefault(cus => cus.ApplicationUserId == userId);
            CustomerDetailsCheckOutViewModel cu = new CustomerDetailsCheckOutViewModel
            {
                Id = cust.Id,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                BillingAddress = cust.BillingAddress,
                BillingCity = cust.BillingCity,
                BillingZip = cust.BillingZip,
                DeliveryAddress = cust.DeliveryAddress,
                DeliveryCity = cust.DeliveryCity,
                DeliveryZip = cust.DeliveryZip,
                EmailAddress = cust.EmailAddress,
                PhoneNo = cust.PhoneNo,
                ApplicationUserId = cust.ApplicationUserId
            };

            CheckOutViewModel model = new CheckOutViewModel
            {
                Movies = movies,
                Customer = cu
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Payment()
        {
            if (TempData["orderViewModel"] != null)
            {
                return View((OrderViewModel)TempData["orderViewModel"]);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Payment(Order order)
        {
            if (order != null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                var o = _context.Orders.ToList().LastOrDefault();

                foreach (var movie in (List<Movie>)Session["CartMovies"])
                {
                    OrderRows orderRows = new OrderRows
                    {
                        MovieId = movie.Id,
                        OrderId = o.Id,
                        Price = movie.Price
                    };

                    _context.OrderRows.Add(orderRows);
                    _context.SaveChanges();
                }
            }

            Session["CartMovies"] = null;
            TempData["Message"] = "Your order has been placed!";
            return RedirectToAction("Orders", "Customer");
        }
    }
}