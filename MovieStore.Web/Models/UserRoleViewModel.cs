using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Models
{
    public class UserRoleViewModel
    {
        public RoleModel Role { get; set; }
    }

    public class RoleModel
    {
        public string Name { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}