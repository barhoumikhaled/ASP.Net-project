namespace FindIt.Migrations
{
    using FindIt.Models;
    using FindIt.Models.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FindIt.Models.Manager;

    internal sealed class Configuration : DbMigrationsConfiguration<FindIt.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FindIt.Models.ApplicationDbContext context)
        {
            //AddRoles(context);
            //AddAdminRole(context);
           // AddProduct(context);
        }
        private void AddRoles(FindIt.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(role => role.Name, new IdentityRole { Name = "Client" });
            context.Roles.AddOrUpdate(role => role.Name, new IdentityRole { Name = "Employee" });
            context.Roles.AddOrUpdate(role => role.Name, new IdentityRole { Name = "Buyer" });
            context.Roles.AddOrUpdate(role => role.Name, new IdentityRole { Name = "Admin" });
            context.SaveChanges();

        }

        private void AddProduct(ApplicationDbContext context)
        {
            List<Product> products = new List<Product> {
                new Product { Name = "Dentifrice", Description = "Dentifrice polyvalent", Price = 4.50 }, 
                new Product { Name = "Jus de fruit", Description = "Jus de fruit des tropiques", Price = 5.99 }, 
                new Product { Name = "Café", Description = "Café Moulu extra fort", Price = 18.85 } 
            };

            context.Products.AddRange(products);
        }

        private void AddAdminRole(FindIt.Models.ApplicationDbContext context)
        {
            //using Microsoft.AspNet.Identity.EntityFramework;
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            //using Microsoft.AspNet.Identity;
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
            //Création d'un compte Admin, s'il n'y en as pas
            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                // Country country = new Country { Name = "Test" };
                // CountryManager.Add(country);
                //  Province province = new Province { Name = "Test", CountryId = 1 };
                //ProvinceManager.Add(province);
                Address address = new Address { No = 999, Street = "None", PostalCode = "Test", City = "Test", Province = new Province { Name = "Québec",
                    Country = new Country { Name = "Canada" } 
                    } 
                };
                AddressManager.Add(address);
                ApplicationUser admin = new ApplicationUser { Email = "admin@none.com", EmailConfirmed = true, UserName = "admin", AdressId = address.Id, Fname = "admin" };
                userManager.Create(admin, "Abc123...");
                context.SaveChanges(); // for AddToRole
                userManager.AddToRole(admin.Id, "Admin");
            }
        }
    }
}
