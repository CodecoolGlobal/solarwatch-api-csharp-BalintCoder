using Solar;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string? State { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Navigation property for the relationship
    public ICollection<SunTimes> SunTimes { get; set; }
}