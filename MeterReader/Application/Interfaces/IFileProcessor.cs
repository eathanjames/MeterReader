using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IFileProcessor
{
    public Task<(List<MeterReadRow> success, List<FileReaderRowError> Errors)> ProcessFileAsync(IFormFile file);
}