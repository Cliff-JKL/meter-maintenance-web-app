using MeterMaintenance.Models;
using MeterMaintenance.Models.Abstract;
using MeterMaintenance.Models.MeterMaintenanceService;
using MeterMaintenance.Services.MeterMaintenanceService;
using Microsoft.AspNetCore.Mvc;

namespace MeterMaintenance.Controllers;

[Route("MeterReading")]
public class MeterReadingController : Controller
{
    private IMeterMaintenanceService MeterMaintenanceService { get; }

    public MeterReadingController(IMeterMaintenanceService meterMaintenanceService)
    {
        MeterMaintenanceService = meterMaintenanceService;
    }
    
    [HttpGet]
    [Route("Update/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var meterReading = await MeterMaintenanceService.GetMeterReadingAsync(id);

        return View(meterReading);
    }
    
    [HttpPost]
    [Route("Update/{id:int}")]
    public async Task<IActionResult> Update(int id, MeterReading meterReading)
    {
        var meterReadingMutation = new Mutation<MeterReadingDTO>
        {
            Entity = new MeterReadingDTO { Value = meterReading.Value },
            Fields = new List<string>() { "Value" }
        };
        
        await MeterMaintenanceService.UpdateMeterReadingAsync(id, meterReadingMutation);

        return RedirectToAction("AllApartments", "Apartment");
    }
}