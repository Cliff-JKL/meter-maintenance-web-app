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
    
    // TODO add filters ?
    public async Task<List<ApartmentWithActualReadingDTO>> GetAllApartmentsWithReadingsAsync()
    {
        var res = await Context.Apartment.AsNoTracking()
            .Select(x => new ApartmentWithActualReadingDTO
            {
                Name = x.Name,
                Reading = x.CurrentMeter.MeterReading.Value,
                ReadingId = x.CurrentMeter.MeterReading.Id,
                ApartmentId = x.Id,
            })
            .ToListAsync();

        return res;
    }

    public async Task<List<Meter>> GetMetersRequiredToCheckAsync(string address)
    {
        var res = await Context.Meter
            .Where(m => m.Apartment.Name.Contains(address))
            .Where(m => m.NextReadingDate <= DateTime.Today)
            .Include(m => m.Apartment)
            .AsNoTracking()
            .ToListAsync();

        return res;
    }

    public async Task<MeterReading> UpdateMeterReadingAsync(int id, Mutation<MeterReadingDTO> mutation)
    {
        var meterReading = await Context.MeterReading
            .AsNoTracking()
            .SingleAsync(m => m.Id == id);

        if (meterReading == null)
        {
            throw new Exception("MeterReading not found!");
        }

        var exceptions = new List<Exception>();

        foreach (var f in mutation.Fields)
        {
            var field = f[0].ToString().ToUpper() + f[1..];
            switch (field)
            {
                case nameof(MeterReadingDTO.Value):
                    meterReading.Value = mutation.Entity.Value;
                    break;
                case nameof(MeterReadingDTO.ReadingDate):
                    meterReading.ReadingDate = mutation.Entity.ReadingDate;
                    break;
                default:
                    exceptions.Add(new Exception($"Cannot update the field: {field}"));
                    break;
            }
        }
        
        if (exceptions.Any())
        {
            throw new AggregateException("Multiple Errors Occured", exceptions);
        }

        Context.MeterReading.Update(meterReading);
        await Context.SaveChangesAsync();
        
        return meterReading;
    }

    public async Task<int?> CreateMeterAsync(MeterDTO meterDTO)
    {
        var meter = new Meter
        {
            SerialNumber = meterDTO.SerialNumber
        };   

        await Context.Meter.AddAsync(meter);
        await Context.SaveChangesAsync();

        return meter.Id;
    }

    public async Task<MeterReading> GetMeterReadingAsync(int id)
    {
        var meterReading = await Context.MeterReading.AsNoTracking().SingleAsync(m => m.Id == id);
        
        if (meterReading == null)
        {
            throw new Exception("MeterReading not found!");
        }

        return meterReading;
    }

    public async Task<List<Meter>> GetNonReservedMetersAsync()
    {
        var meters = await Context.Meter
            .AsNoTracking()
            .Include(m => m.Apartment)
            .Include(m => m.MeterReplacementHistory)
            .Include(m => m.MeterReading)
            .Where(m => m.Apartment == null && m.MeterReplacementHistory == null && m.MeterReading == null)
            .ToListAsync();

        return meters;
    }
    
    public async Task<Apartment> GetApartmentAsync(int id)
    {
        var apartment = await Context.Apartment
            .AsNoTracking()
            .SingleAsync(a => a.Id == id);
        
        if (apartment == null)
        {
            throw new Exception("Apartment not found!");
        }

        return apartment;
    }
    
    public async Task<Apartment> UpdateApartmentMeterAsync(int id, ApartmentDTO apartmentDto)
    {
        var meterId = apartmentDto.CurrentMeterId ?? -1;
        if (meterId == -1)
        {
            throw new Exception("meterId value is null!");
        }

        var meter = await Context.Meter.AsNoTracking().SingleAsync(m => m.Id == meterId);
        if (meter == null)
        {
            throw new Exception("Meter not found!");
        }

        // set new reading dates for meter
        meter.NextReadingDate = DateTime.Today.AddMonths(1);
        meter.LastReadingDate = DateTime.Today;

        var apartment = await Context.Apartment
            .AsNoTracking()
            .Include(a => a.CurrentMeter)
            .ThenInclude(cm => cm.MeterReading)
            .SingleAsync(a => a.Id == id);
        
        if (apartment == null)
        {
            throw new Exception("Apartment not found!");
        }

        // set new meter
        apartment.CurrentMeterId = meterId;

        var meterReading = new MeterReading
        {
            Value = 0,
            ReadingDate = DateTime.Today,
            MeterId = meterId,
        };

        var history = await Context.MeterReplacementHistories
            .AsNoTracking()
            .Where(h => h.ApartmentId == id)
            .ToListAsync();
        
        // create new meter replacement history record
        var meterReplacementHistory = new MeterReplacementHistory
        {
            ApartmentId = apartment.Id,
            CreationDate = DateTime.Today,
            OldMeterReadingValue = apartment.CurrentMeter?.MeterReading?.Value,
            NewMeterId = history.Count > 0 ? meterId : null
        };

        apartment.CurrentMeter = meter;

        Context.Meter.Update(meter);
        await Context.MeterReading.AddAsync(meterReading);
        await Context.MeterReplacementHistories.AddAsync(meterReplacementHistory);
        Context.Apartment.Update(apartment);
        await Context.SaveChangesAsync();
        
        return apartment;
    }
    
    public async Task<List<MeterReplacementHistory>> GetApartmentMeterReplacementHistoryAsync(int apartmentId)
    {
        var res = await Context.MeterReplacementHistories
            .Where(h => h.ApartmentId == apartmentId)
            .AsNoTracking()
            .Include(h => h.NewMeter)
            .ToListAsync();

        return res;
    }
}