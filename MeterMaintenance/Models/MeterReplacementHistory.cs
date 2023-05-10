namespace MeterMaintenance.Models;

public class MeterReplacementHistory
{
    public int Id { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public double? OldMeterReadingValue { get; set; }
    
    public int? NewMeterId { get; set; }
    public Meter? NewMeter { get; set; }
    
    public int ApartmentId { get; set; }
    public Apartment Apartment { get; set; } = null!;
}