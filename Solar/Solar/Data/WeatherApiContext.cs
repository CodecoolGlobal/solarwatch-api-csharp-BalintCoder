using Microsoft.EntityFrameworkCore;

namespace Solar.Data;

public class WeatherApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunTimes> SunTimes { get; set; }

    public WeatherApiContext(DbContextOptions<WeatherApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Configure the City entity - making the 'Name' unique
        base.OnModelCreating(builder);
        builder.Entity<City>(i =>
        {
            i.HasKey(c => c.Id);
            i.HasIndex(c => c.Name).IsUnique();
            i.HasMany(c => c.SunTimes).WithOne(s => s.City).HasForeignKey(s => s.CityId).OnDelete(DeleteBehavior.Cascade);
        });




       

    }
}