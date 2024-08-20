using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Data
{
    public class HotelListingDbContext:DbContext
    {
        public HotelListingDbContext(DbContextOptions options):base(options)    
        {
           
        }

        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Country> countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country {
                  Id = 1,
                  Name = "China",
                  ShortName = "CN"
                },
                new Country { 
                  Id = 2,
                  Name = "Janpan",
                  ShortName = "JP"
                }
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort",
                    Address = "Sichuan",
                    CountryId = 1,
                    Rating = 4.5
                }
            );
        }
    }
}
