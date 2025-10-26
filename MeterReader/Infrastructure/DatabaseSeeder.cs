using System.Globalization;
using CsvHelper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class DatabaseSeeder(AppDbContext context, ILogger<DatabaseSeeder> logger)
{
    public async Task SeedDataAsync()
    {
        if (await context.MeterReadings.AnyAsync())
        {
            await context.MeterReadings.ExecuteDeleteAsync();
        }
        
        
        if (await context.Accounts.AnyAsync())
        {
            logger.LogInformation("Accounts already seeded");
            return;
        }
        

        var path = Path.Combine(AppContext.BaseDirectory, "Data", "test-accounts.csv");

        if (!File.Exists(path))
        {
            logger.LogWarning("Path {Path} not found.", path);
            return;
        }
        
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        var records = csv.GetRecords<Account>().ToList();

        context.Accounts.AddRange(records);
        await context.SaveChangesAsync();
        
        
        logger.LogInformation("Seeded {Count} products from CSV.", records.Count);

    }
}