namespace MeterMaintenance.Models;

public class Apartment
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int? CurrentMeterId { get; set; }
    public Meter? CurrentMeter { get; set; }
    
    public ICollection<MeterReplacementHistory>? MeterReplacementHistories { get; set; }
}