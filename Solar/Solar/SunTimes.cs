namespace Solar;

public class SunTimes
{
    
    public string City { get; set; }
    public DateOnly Date { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public string? TimeZone { get; set; }
}