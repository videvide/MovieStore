using MovieStore.Lib.Models;
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

        public Customer GetCustomer (string userId)
        {
            var user = _context.Customers.FirstOrDefault(c => c.ApplicationUserId == userId);
            
            return user;
           
        }

        public Customer GetCustomerByCustomerId(int customerId)
        {
            var user = _context.Customers.FirstOrDefault(c => c.Id == customerId);
            return user;
        }
    }
}