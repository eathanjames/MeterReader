using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Config;

public static class DependencyInjection
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddScoped<DatabaseSeeder>();
        builder.Services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    }

    public static void AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IMeterReadingService, MeterReadingService>();
    }

}