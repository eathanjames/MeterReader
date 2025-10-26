using Application.DTO;
using Application.Interfaces;
using Application.Mappings;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class MeterReadingService(IFileProcessor processor, IMeterReadingValidator validator, IMeterReadingRepository meterReadingRepository) : IMeterReadingService
{
    public async Task<FileReaderUploadResult<MeterReadRow>> ProcessMeterReadingFileAsync(IFormFile file)
    { 
        var result = new FileReaderUploadResult<MeterReadRow>();
        var successes = new List<MeterReadRow>();
        var errors = new List<FileReaderRowError>();
        try
        {
            var processedFiles = await processor.ProcessFileAsync(file);
            successes = processedFiles.success;
            errors = processedFiles.Errors;
        }
        catch(FileReaderException ex)
        {
            //This needs to bubble up later.
            Console.WriteLine(ex.Message);
        }

        foreach (var row in successes.ToList())
        {
            var validate = await validator.ValidateAsync(row);
            if (validate.success)
            {
                await meterReadingRepository.CreateAsync(row.ToMeterReading());
            }
            else
            {
                successes.Remove(row);
                errors.Add(new FileReaderRowError(
                    row.RowNumber,
                    validate.Errors.FirstOrDefault() ?? "Unknown error",
                    $"{row.AccountId}, {row.MeterReadingDateTime}, {row.MeterReadValue}"
                ));
            }
        }

        result.Successful = successes;
        result.Failures = errors;
        
        return result;
    }

    public async Task<IEnumerable<MeterReading>> GetAllAsync()
    {
        return await meterReadingRepository.GetAllAsync();
    }
}