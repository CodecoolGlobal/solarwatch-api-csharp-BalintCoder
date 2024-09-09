public class SunTimes
{
    public int Id { get; set; } // Primary key
    public DateTime Date { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public string? TimeZone { get; set; }
    
    public int CityId { get; set; } // Foreign key

    // Navigation property
    public City City { get; set; }
}