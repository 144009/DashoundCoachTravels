namespace DashoundCoachTravels.Migrations
{
    using DashoundCoachTravels.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DashoundCoachTravels.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DashoundCoachTravels.Models.ApplicationDbContext context)
        {
            var userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Creating new role types for accounts
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            IdentityResult roleResult;

            // Adding new role types named: Administrator, Employee, User
            if (!roleManager.RoleExists("Administrator")) { roleResult = roleManager.Create(new IdentityRole("Administrator")); }
            if (!roleManager.RoleExists("Employee")) { roleResult = roleManager.Create(new IdentityRole("Employee")); }
            if (!roleManager.RoleExists("User")) { roleResult = roleManager.Create(new IdentityRole("User")); }
            //Adding sample users: 1 for each role type testing only: Basic|Employee|Admin
            var sampleAccount = userManager.FindByEmail("user@gmail.com");
            if (sampleAccount == null)
            {
                var hasher = new PasswordHasher();
                sampleAccount = new ApplicationUser()
                {
                    UserName = "user",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword("user"),
                    Name = "Jon",
                    Surname = "Doe",
                    Town = "aaa",
                    Street = "streetA",
                    NumHouse = "1",
                    NumFlat = "12",
                    ZIPCode = "22-222",
                    Country = "Uganda"
                };
                userManager.Create(sampleAccount);
                userManager.AddToRole(sampleAccount.Id, "User");
                context.SaveChanges();
            }
            sampleAccount = userManager.FindByEmail("employee@gmail.com");
            if (sampleAccount == null)
            {
                var hasher = new PasswordHasher();
                sampleAccount = new ApplicationUser()
                {
                    UserName = "employee",
                    Email = "employee@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword("employee"),
                    Name = "Jonatan",
                    Surname = "Doppey",
                    Town = "bbb",
                    Street = "streetB",
                    NumHouse = "2",
                    NumFlat = "186",
                    ZIPCode = "66-666",
                    Country = "Nigeria"
                };
                userManager.Create(sampleAccount);
                userManager.AddToRole(sampleAccount.Id, "Employee");
                context.SaveChanges();
            }
            sampleAccount = userManager.FindByEmail("admin@gmail.com");
            if (sampleAccount == null)
            {
                var hasher = new PasswordHasher();

                sampleAccount = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword("admin"),
                    Name = "Jonny",
                    Surname = "Dope",
                    Town = "ccc",
                    Street = "streetC",
                    NumHouse = "79",
                    NumFlat = "6",
                    ZIPCode = "99-971",
                    Country = "Egypt"
                };
                userManager.Create(sampleAccount);
                userManager.AddToRole(sampleAccount.Id, "Administrator");
                context.SaveChanges();
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
