using MeterMaintenance.Services.MeterMaintenanceService;
using Microsoft.AspNetCore.Mvc;

namespace MeterMaintenance.Controllers;

public class MeterController : Controller
{
    private IMeterMaintenanceService MeterMaintenanceService { get; }
    
    public MeterController(IMeterMaintenanceService meterMaintenanceService)
    {
        MeterMaintenanceService = meterMaintenanceService;
    }
    
    [Route("Meter/CheckRequired/{street}/{house}")]
    public async Task<IActionResult> CheckRequired(string street, string house)
    {
        var res = await MeterMaintenanceService.GetMetersRequiredToCheck($"{street}/{house}");
        
        return View(res);
    }
}