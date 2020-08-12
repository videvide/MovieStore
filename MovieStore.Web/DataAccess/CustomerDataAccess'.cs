using MovieStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Web.DataAccess
{
    public class CustomerDataAccess
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public string GetFullName(string userId)
        {
            var user = _context.Customers.FirstOrDefault(c => c.ApplicationUserId == userId);
            string output = null;

            if (user != null)
            {
                output = $"{user.FirstName} {user.LastName}";
            }

            return output;
        }
    }
}