
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tms_inc.Models;
using tms_inc.Models.Employees;
using tms_inc.Models.File;
using tms_inc.Models.Position;
using tms_inc.Models.RequestedQuote;
using tms.Models;

namespace tms.DataAccess
{
    public class PortalContext : IdentityDbContext<User>
    {
        public PortalContext(DbContextOptions<PortalContext> options)
           : base(options)
        {
            // Database.EnsureDeleted();
            //don't uncomment
            //Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>().HasOne(x => x.User).WithMany(x => x.Drivers).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Truck>().HasOne(x => x.Driver).WithOne(x => x.Truck).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Trailer>().HasOne(x => x.Driver).WithOne(x => x.Trailer).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DriverCity>().HasOne(x => x.City).WithMany(x => x.DriverCities).HasForeignKey(x => x.CityId);
            modelBuilder.Entity<DriverCity>().HasOne(x => x.Driver).WithMany(x => x.DriverCities).HasForeignKey(x => x.DriverId);
            
            modelBuilder.Entity<DriverCity>().HasIndex(x => new { x.DriverId, x.Date }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
        // public  DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverCity> DriverCities { get; set; }
        public DbSet<Applicant> People { get; set; }
        public DbSet<PersonFile> PersonFiles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<GetInTouch> GetInTouches { get; set; }
        public DbSet<RequestedQuote>  RequestedQuotes { get; set; }
    }

}
