using MeterMaintenance.Models;
using MeterMaintenance.Models.Abstract;
using MeterMaintenance.Models.MeterMaintenanceService;

namespace MeterMaintenance.Services.MeterMaintenanceService;

public interface IMeterMaintenanceService
{
    // TODO add filters
    Task<List<ApartmentWithActualReadingDTO>> GetAllApartmentsWithReadings();

    Task<List<Meter>> GetMetersRequiredToCheck(string houseAddress);

    Task<MeterReading> UpdateMeterReading(int apartmentId, Mutation<MeterReadingDTO> mutation);
}