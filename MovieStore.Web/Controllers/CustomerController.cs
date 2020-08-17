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

namespace MovieStore.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Orders(string success)
        {
            if (success == "true")
            {
                TempData["Message"] = "Your order has been placed";
                return RedirectToAction("Orders");
            }
            return View();
        }
    }
}