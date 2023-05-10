using MeterMaintenance.Models.ViewModels;
using MeterMaintenance.Services.MeterMaintenanceService;
using Microsoft.AspNetCore.Mvc;

namespace MeterMaintenance.Controllers;

public class MeterReplacementHistoryController : Controller
{
    private IMeterMaintenanceService MeterMaintenanceService { get; }

    public MeterReplacementHistoryController(IMeterMaintenanceService meterMaintenanceService)
    {
        MeterMaintenanceService = meterMaintenanceService;
    }
    
    [HttpGet]
    [Route("MeterReplacementHistory/{id:int}")]
    public async Task<IActionResult> Index(int id)
    {
        var res = await MeterMaintenanceService.GetApartmentMeterReplacementHistoryAsync(id);

        return View(res);
    }
}