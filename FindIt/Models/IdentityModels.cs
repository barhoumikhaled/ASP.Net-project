using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FindIt.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;


namespace FindIt.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int AdressId { get; set; }
        public virtual Address Adress { get; set; }
        public int? DeliveryAddressId { get; set; }
        public virtual Address DeliveryAddress { get; set; }
        [NotMapped]
        public bool Delivery { get; set; }
        public virtual ICollection<CommandeClient> ClientsCommandes { get; set; }
        public virtual ICollection<CommandeSupplier> SupplierCommandes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Keyword> Keyword { get; set; }
        public virtual DbSet<CommandeClient> CommandeClient { get; set; }
        public virtual DbSet<CommandeSupplier> CommandeSupplier { get; set; }
        public virtual DbSet<EntryCommandeClient> EntryCommandeClient { get; set; }
        public virtual DbSet<EntryCommandeSupplier> EntryCommandeSupplier { get; set; }
        public virtual DbSet<MainCategories> MainCategories { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SubCategorie> SubCategorie { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<File> Files { get; set; }
    }
}