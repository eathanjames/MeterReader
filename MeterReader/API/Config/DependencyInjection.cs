using Application.Interfaces;
using Application.Parser;
using Application.Services;
using Application.Validation;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Config;

public static class DependencyInjection
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        AddInfrastructure(builder);
        AddApplication(builder);
    }
    
    private static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddScoped<DatabaseSeeder>();
        builder.Services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    }

    private static void AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IFileProcessor, FileProcessor>();
        builder.Services.AddScoped<IFileReader, CsvFileReader>();
        builder.Services.AddScoped<IMeterReadingValidator, MeterReadingValidator>();
        builder.Services.AddScoped<IMeterReadingService, MeterReadingService>();
    }

}