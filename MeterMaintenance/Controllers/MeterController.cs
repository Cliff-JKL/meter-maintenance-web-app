using MeterMaintenance.Models.MeterMaintenanceService;
using MeterMaintenance.Models.ViewModels;
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
    
    public async Task<IActionResult> Index()
    {
        var apartments = await MeterMaintenanceService.GetAllApartmentsWithReadingsAsync();
        var houses = apartments
            .Select(a => a.Name[..a.Name.LastIndexOf('/')])
            .Distinct();
        
        var res = new MeterIndex
        {
            Houses = houses,
            Address = houses.First()
        };
        
        return View(res);
    }
    
    [Route("Meter/CheckRequired/{Address}")]
    public async Task<IActionResult> CheckRequired(string Address)
    {
        var res = await MeterMaintenanceService.GetMetersRequiredToCheckAsync(Address);
        
        return View(res);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var meter = new MeterDTO();

        return View(meter);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MeterDTO meter)
    {
        await MeterMaintenanceService.CreateMeterAsync(meter);

        return RedirectToAction("Index");
    }
}