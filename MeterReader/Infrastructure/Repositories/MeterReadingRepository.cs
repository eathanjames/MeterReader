using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MeterReadingRepository(AppDbContext context) : IMeterReadingRepository
{ 
    public async Task CreateAsync(MeterReading meterReading)
    {
        await context.MeterReadings.AddAsync(meterReading);
        
        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<MeterReading>> GetAllAsync()
    {
        return await context.MeterReadings.Take(100).ToListAsync();
    }
    
    public async Task<bool> ExistsAsync(int accountId, DateTime date, string meterReading)
    {
        return await context.MeterReadings.AnyAsync(a =>
                a.AccountId == accountId && a.MeterReadingDateTime == date && a.MeterReadValue == meterReading);
    }
    
    public async Task<bool> NewerExistsAsync(int accountId, DateTime date, string meterReading)
    {
        return await context.MeterReadings.AnyAsync(a =>
            a.AccountId == accountId && a.MeterReadingDateTime < date && a.MeterReadValue == meterReading);
    }
}