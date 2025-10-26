using Application.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IMeterReadingService
{
    public Task<FileReaderUploadResult<MeterReadRow>> ProcessMeterReadingFileAsync(IFormFile file);
    public Task<IEnumerable<MeterReading>> GetAllAsync();
}