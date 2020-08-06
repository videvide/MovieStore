using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieStore.Lib.Models;
using MovieStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Roles
        public ActionResult Roles()
        {
            var roles = _context.Roles.ToList();

            List<UserRoleViewModel> userRoleList = new List<UserRoleViewModel>();

            foreach (var role in roles)
            {
                List<IdentityUser> userList = new List<IdentityUser>();

                foreach (var user in role.Users)
                {
                    var usr = _context.Users.FirstOrDefault(us => us.Id == user.UserId);

                    userList.Add(usr);
                }

                var u = new UserRoleViewModel
                {
                    Role = new RoleModel
                    {
                        Name = role.Name,
                        Users = userList
                    }
                };

                userRoleList.Add(u);
            }

            return View(userRoleList);
        }

        // GET: Admin/Customers
        public ActionResult Customers()
        {
            var customers = _context.Customers.ToList();

            return View(customers);
        }

        // GET: Admin/Customers/ID
        public ActionResult CustomerDetails(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            return View(customer);
        }

        // GET: Admin/EditCustomer/ID
        public ActionResult EditCustomer(int id)
        {
            var user = _context.Customers.FirstOrDefault(c => c.Id == id);

            return View(user);
        }

        // POST: Admin/EditCustomer/CustomerData
        [HttpPost]
        public ActionResult EditCustomer([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingZip,BillingCity,DeliveryAddress,DeliveryZip,DeliveryCity,EmailAddress,PhoneNo")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("CustomerDetails", new { id = customer.Id });
            }

            return View(customer);
        }
    }
}