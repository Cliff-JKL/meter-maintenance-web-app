using MeterMaintenance.Models;
using MeterMaintenance.Models.Abstract;
using MeterMaintenance.Models.MeterMaintenanceService;

namespace MeterMaintenance.Services.MeterMaintenanceService;

public interface IMeterMaintenanceService
{
    // Получить список всех квартир с показаниями, актуальными на сегодня
    Task<List<ApartmentWithActualReadingDTO>> GetAllApartmentsWithReadingsAsync();

    // Получить список счетчиков для выбранного дома, для которых требуется проверка 
    Task<List<Meter>> GetMetersRequiredToCheckAsync(string address);

    Task<MeterReading> UpdateMeterReadingAsync(int apartmentId, Mutation<MeterReadingDTO> mutation);

    // Создание нового счетчика
    Task<int?> CreateMeterAsync(MeterDTO meter);

    // Обновление показаний для счетчика в квартире
    Task<MeterReading> GetMeterReadingAsync(int id);

    Task<List<Meter>> GetNonReservedMetersAsync();

    Task<Apartment> GetApartmentAsync(int id);

    // Замена счетчика в квартире на новый
    Task<Apartment> UpdateApartmentMeterAsync(int id, ApartmentDTO apartmentDto);

    // Получить историю замен счетчиков для заданной квартиры
    Task<List<MeterReplacementHistory>> GetApartmentMeterReplacementHistoryAsync(int apartmentId);
}