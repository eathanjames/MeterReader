using Application.DTO;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Parser;

public class FileProcessor(IEnumerable<IFileReader> readers) : IFileProcessor
{
    public async Task<(List<MeterReadRow> success, List<FileReaderRowError> Errors)> ProcessFileAsync(IFormFile file)
    {
        var match = readers.FirstOrDefault(r => r.CanRead(file));

        if (match is null)
        {
            throw new FileReaderException("No reader found for file"); 
        }
        
        return await match.ReadAsync(file);
    }
}