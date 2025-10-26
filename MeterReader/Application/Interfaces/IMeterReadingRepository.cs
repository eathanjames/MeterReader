using Domain.Entities;

namespace Application.Interfaces;

public interface IMeterReadingRepository
{
    public Task CreateAsync(MeterReading meterReading);
    public Task<IEnumerable<MeterReading>> GetAllAsync();
    public Task<bool> ExistsAsync(int accountId, DateTime date, string meterReading);
    public Task<bool> NewerExistsAsync(int accountId, DateTime date, string meterReading);
}