namespace MeterMaintenance.Models.MeterMaintenanceService;

public class ApartmentWithActualReadingDTO
{
    public string Name { get; set; }
    
    public double? Reading { get; set; }
    
    public int? ReadingId { get; set; }
    
    public int? ApartmentId { get; set; }
}