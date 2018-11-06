using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DashoundCoachTravels.Models.DBEntities; // so new classes can be seen here

namespace DashoundCoachTravels.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // In this place im adding remaining attributes to AspNetUsers Entity
        [Required(ErrorMessage = "Enter your name.")]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your surname.")]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your country")]
        [StringLength(100)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter your town")]
        [StringLength(100)]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Enter your street name")]
        [StringLength(100)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Enter your house number.")]
        [Display(Name = "House number")]
        public string NumHouse { get; set; }

        [Display(Name = "Flat number (optional)")]
        public string NumFlat { get; set; }

        [Required(ErrorMessage = "Enter your ZIP code.")]
        [StringLength(100)]
        [Display(Name = "ZIP Code")]
        public string ZIPCode { get; set; }
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
        // in this place im adding all manually created entities from /Models/DBEntites/ so they will be migrated to a DB
        // DBSet<model (class file) name> <table name in DB>
        public DbSet<Booking> Reservations { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Trip_Locations> Trip_Locations { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}