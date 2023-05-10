namespace MeterMaintenance.Models;

public class Meter
{
    public int Id { get; set; }
    
    public string SerialNumber { get; set; }
    
    public DateTime? LastReadingDate { get; set; }
    
    public DateTime? NextReadingDate { get; set; }

    public Apartment? Apartment { get; set; }

    public MeterReading? MeterReading { get; set; }
    
    // not in use
    public MeterReplacementHistory? MeterReplacementHistory { get; set; }
}