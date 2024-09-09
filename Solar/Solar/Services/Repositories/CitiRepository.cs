using Solar;
using Solar.Data;

public class CityRepository : ICityRepository
{
    private WeatherApiContext dbContext;

    public CityRepository(WeatherApiContext context)
    {
        dbContext = context;
    }

    public IEnumerable<City> GetAll()
    {
        return dbContext.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        return dbContext.Cities.FirstOrDefault(c => c.Name == name);
    }

    public void Add(City city)
    {
        dbContext.Add(city);
        dbContext.SaveChanges();
    }

    public void Delete(City city)
    {
        dbContext.Remove(city);
        dbContext.SaveChanges();
    }

    public void Update(City city)
    {  
        dbContext.Update(city);
        dbContext.SaveChanges();
    }
}