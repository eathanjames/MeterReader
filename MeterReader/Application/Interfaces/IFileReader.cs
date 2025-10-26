using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IFileReader
{
    bool CanRead(IFormFile file);
    
    Task<(List<MeterReadRow> success, List<FileReaderRowError> Errors)> ReadAsync(IFormFile file);
}