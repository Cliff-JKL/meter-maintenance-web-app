using MeterMaintenance.Services.MeterMaintenanceService;
using Microsoft.AspNetCore.Mvc;

namespace MeterMaintenance.Controllers;

public class ApartmentController : Controller
{
    private IMeterMaintenanceService MeterMaintenanceService { get; }
    
    public ApartmentController(IMeterMaintenanceService meterMaintenanceService)
    {
        MeterMaintenanceService = meterMaintenanceService;
    }
    
    public async Task<IActionResult> AllApartments()
    {
        var res = await MeterMaintenanceService.GetAllApartmentsWithReadings();
        
        return View(res);
    }
}