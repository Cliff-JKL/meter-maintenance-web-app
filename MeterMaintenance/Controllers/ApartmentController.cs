using MeterMaintenance.Models;
using MeterMaintenance.Models.MeterMaintenanceService;
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

    [HttpGet]
    [Route("Apartment/Update/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var meters = await MeterMaintenanceService.GetNonReservedMeters();
        var apartment = await MeterMaintenanceService.GetApartmentAsync(id);
        ViewData["Meters"] = meters;
        
        return View(apartment);
    }
    
    [HttpPost]
    [Route("Apartment/Update/{id:int}")]
    public async Task<IActionResult> Update(int id, Apartment apartment)
    {
        var apartmentDto = new ApartmentDTO
        {
            CurrentMeterId = apartment.CurrentMeterId,
        };
        
        await MeterMaintenanceService.UpdateApartmentMeter(id, apartmentDto);

        return RedirectToAction("AllApartments", "Apartment");
    }
}