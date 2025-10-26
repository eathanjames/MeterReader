using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class MeterReaderEndpoints
{
    private const string Route = "api";
    private const string Tag = "MeterReader";


    public static void Map(WebApplication app)
    {
        app.MapPost($"{Route}/meter-reading-uploads", (
                [FromServices] IMeterReadingService meterService,
                IFormFile file) => meterService.ProcessMeterReadingFileAsync(file))
            .Produces<FileReaderUploadResult<MeterReadRow>>()
            .Produces(404)
            .WithTags(Tag)
            .WithDescription("Get account by id")
            .DisableAntiforgery();
        
        app.MapGet($"{Route}", (
                [FromServices] IMeterReadingService meterService) => meterService.GetAllAsync())
            .Produces<IEnumerable<MeterReading>>()
            .Produces(404)
            .WithTags(Tag)
            .WithDescription("Get account by id")
            .DisableAntiforgery();
    }
}