using MeterMaintenance.Models;
using MeterMaintenance.Models.Abstract;
using MeterMaintenance.Models.MeterMaintenanceService;

namespace MeterMaintenance.Services.MeterMaintenanceService;

public interface IMeterMaintenanceService
{
    // Получить список всех квартир с показаниями, актуальными на сегодня
    Task<List<ApartmentWithActualReadingDTO>> GetAllApartmentsWithReadings();

    // Получить список счетчиков для выбранного дома, для которых требуется проверка 
    Task<List<Meter>> GetMetersRequiredToCheck(string address);

    Task<MeterReading> UpdateMeterReading(int apartmentId, Mutation<MeterReadingDTO> mutation);

    Task<int?> CreateMeterAsync(MeterDTO meter);

    Task<MeterReading> GetMeterReadingAsync(int id);

    Task<List<Meter>> GetNonReservedMeters();

    Task<Apartment> GetApartmentAsync(int id);

    Task<Apartment> UpdateApartmentMeter(int id, ApartmentDTO apartmentDto);

    // Получить историю замен счетчиков для квартиры
    Task<List<MeterReplacementHistory>> GetApartmentMeterReplacementHistoryAsync(int apartmentId);
}