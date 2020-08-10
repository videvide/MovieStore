namespace MovieStore.Web.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieStore.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MovieStore.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (context.Roles.FirstOrDefault(r => r.Name == "Admin") == null)
            {
                var adminRole = new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin"
                };
                context.Roles.AddOrUpdate(adminRole);
                context.SaveChanges();
            }

            if (context.Roles.FirstOrDefault(r => r.Name == "Customer") == null)
            {
                var customerRole = new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Customer"
                };
                context.Roles.AddOrUpdate(customerRole);
                context.SaveChanges();
            }
        }
    }
}