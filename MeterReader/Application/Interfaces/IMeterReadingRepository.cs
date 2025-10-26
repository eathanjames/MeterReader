using Domain.Entities;

namespace Application.Interfaces;

public interface IMeterReadingRepository
{
    public Task CreateAsync(MeterReading meterReading);
    public Task<MeterReading?> GetAsync(int accountId);
    public Task<IEnumerable<MeterReading>> GetAllAsync();
    public Task<bool> UpdateAsync(MeterReading meterReading);
}