namespace MeterMaintenance.Models;

public class MeterReading
{
    public int Id { get; set; }
    
    public double Value { get; set; }
    
    public DateTime ReadingDate { get; set; }
    
    public int MeterId { get; set; }
    public Meter Meter { get; set; } = null!;
}