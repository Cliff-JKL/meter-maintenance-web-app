namespace MeterMaintenance.Models;

public class Apartment
{
    public int Id { get; set; }
    
    // TODO to XPath
    public String Name { get; set; }
    
    public int? CurrentMeterId { get; set; }
    public Meter? CurrentMeter { get; set; }
}