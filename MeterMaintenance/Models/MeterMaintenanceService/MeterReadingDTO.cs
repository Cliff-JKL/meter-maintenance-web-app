namespace MeterMaintenance.Models.MeterMaintenanceService;

public class MeterReadingDTO
{
    // TODO only positive numbers
    public double Value { get; set; }
    
    public DateTime ReadingDate { get; set; }
}