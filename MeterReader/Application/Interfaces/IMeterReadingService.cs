using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IMeterReadingService
{
    public Task<MeterReadingUploadResult> ProcessFileAsync(IFormFile file);
}