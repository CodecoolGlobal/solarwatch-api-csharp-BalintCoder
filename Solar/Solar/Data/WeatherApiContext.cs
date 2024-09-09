using Microsoft.EntityFrameworkCore;

namespace Solar.Data;

public class WeatherApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }

    public WeatherApiContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Configure the City entity - making the 'Name' unique
        builder.Entity<City>()
            .HasIndex(u => u.Name)
            .IsUnique();
    
        builder.Entity<City>()
            .HasData(
                new City { Id = 1, Name = "London", Latitude = 51.509865, Longitude = -0.118092 },
                new City { Id = 2, Name = "Budapest", Latitude = 47.497913, Longitude = 19.040236 },
                new City { Id = 3, Name = "Paris", Latitude = 48.864716, Longitude = 2.349014 }
            );
    }
}