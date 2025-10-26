using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class MeterReadingRepository(AppDbContext context) : IMeterReadingRepository
{ 
    public async Task CreateAsync(MeterReading meterReading)
    {
        await context.MeterReadings.AddAsync(meterReading);
        
        await context.SaveChangesAsync();
    }

    public Task<MeterReading?> GetAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MeterReading>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(MeterReading meterReading)
    {
        throw new NotImplementedException();
    }
}