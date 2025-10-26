using System.Globalization;
using Application.DTO;
using Application.Interfaces;
using Application.Mappings;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class MeterReadingService : IMeterReadingService
{
    public async Task<MeterReadingUploadResult> ProcessFileAsync(IFormFile file)
    { 
        var result = new MeterReadingUploadResult();
        var records = new List<MeterReadRow>();
        var failures = new List<RowError>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            //Make header lower to prevent issues with possible inconsistent header namings.
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            BadDataFound = null,
            MissingFieldFound = null
        };
        
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<MeterReadCsvMap>();
        
        await csv.ReadAsync();
        csv.ReadHeader();
        
        var expectedColumns = csv.HeaderRecord?.Length ?? 3;
        
        while (await csv.ReadAsync())
        {
            var actualColumns = csv.Context.Parser.Count;
            if (actualColumns != expectedColumns)
            {
                failures.Add(new RowError(csv.Context.Parser.Row,
                    $"Incorrect number of columns expected: {expectedColumns}, actual: {actualColumns}",
                    csv.Context.Parser.RawRecord)
                );
                continue;
            }

            try
            {
                var record = csv.GetRecord<MeterReadRow>();
                records.Add(record);
            }
            catch (Exception e)
            {
                failures.Add(new RowError(csv.Context.Parser.Row,
                    $"Encountered error {e.Message}.",
                    csv.Context.Parser.RawRecord)
                );
            }
        }
        
        result.Successful = records;
        result.Failures = failures; 
        
        return result;
    }
}