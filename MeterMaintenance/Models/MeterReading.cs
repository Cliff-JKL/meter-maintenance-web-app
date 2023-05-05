namespace MeterMaintenance.Models;

public class MeterReading
{
    public int Id { get; set; }
    
    // TODO only positive numbers
    public double Value { get; set; }
    
    public DateTime ReadingDate { get; set; }
    
    public int? MeterId { get; set; }
    public Meter Meter { get; set; }
}