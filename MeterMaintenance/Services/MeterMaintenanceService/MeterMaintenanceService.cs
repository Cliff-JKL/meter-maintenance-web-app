using MeterMaintenance.EF;
using MeterMaintenance.Models;
using MeterMaintenance.Models.Abstract;
using MeterMaintenance.Models.MeterMaintenanceService;
using Microsoft.EntityFrameworkCore;

namespace MeterMaintenance.Services.MeterMaintenanceService;

public class MeterMaintenanceService : IMeterMaintenanceService
{
    private IMeterMaintenanceContext Context { get; }
    
    public MeterMaintenanceService(IMeterMaintenanceContext context)
    {
        Context = context;
    }
    
    public async Task<List<ApartmentWithActualReadingDTO>> GetAllApartmentsWithReadings()
    {
        var meters = await Context.Meter.AsNoTracking().ToListAsync();

        var apartments = await Context.Apartment.AsNoTracking().ToListAsync();

        var res = await Context.Apartment.AsNoTracking()
            .Select(x => new ApartmentWithActualReadingDTO
            {
                Name = x.Name,
                Reading = x.CurrentMeter.MeterReading.Value,
            })
            .ToListAsync();

        return res;
    }

    public async Task<List<Meter>> GetMetersRequiredToCheck(string houseAddress)
    {
        var res = await Context.Meter
            .Where(m => m.Apartment.Name.Contains(houseAddress))
            .Where(m => m.NextReadingDate >= DateTime.Today)
            .AsNoTracking()
            .ToListAsync();

        return res;
    }

    public Task<MeterReading> UpdateMeterReading(int apartmentId, Mutation<MeterReadingDTO> mutation)
    {
        throw new NotImplementedException();
    }
}