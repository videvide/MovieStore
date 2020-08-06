using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieStore.Web.Models;
using System;
using System.Collections.Generic;
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

        // Admin/Customers/ID
        public ActionResult CustomerDetails(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            return View(customer);
        }
    }
}