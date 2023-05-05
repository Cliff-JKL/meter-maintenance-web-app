namespace MeterMaintenance.Models.MeterMaintenanceService;

public class ApartmentWithActualReadingDTO
{
    // TODO xpath
    public string Name { get; set; }
    
    // TODO only positive number
    public double? Reading { get; set; }
}