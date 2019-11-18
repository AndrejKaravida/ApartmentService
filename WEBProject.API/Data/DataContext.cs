using Microsoft.EntityFrameworkCore;
using WEBProject.API.Models;

namespace WEBProject.API.Data
{
    public class DataContext : DbContext
    {
          public DataContext(DbContextOptions<DataContext>  options) : base (options) {}

            public DbSet<User> Users { get; set; }
            public DbSet<Amentity> Amentities { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Location> Location { get; set; }
            public DbSet<Apartment> Apartments { get; set; }
            public DbSet<Address> Addresses { get; set; }
            public DbSet<Reservation> Reservations { get; set; }
            public DbSet<Photo> Photos { get; set; }
         //   public DbSet<ReservedDate> ReservedDates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
            

         
    }
}