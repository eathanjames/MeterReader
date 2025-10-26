using System.Globalization;
using Application.DTO;
using Application.Interfaces;
using Application.Mappings;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace Application.Parser;

public class CsvFileReader : IFileReader
{
    private static readonly List<string> ContentTypes = ["text/csv"];

    public bool CanRead(IFormFile file)
    {
        return ContentTypes.Contains(file.ContentType);
    }

    public async Task<(List<MeterReadRow> success, List<FileReaderRowError> Errors)> ReadAsync(IFormFile file)
    {
        var successes = new List<MeterReadRow>();
        var errors = new List<FileReaderRowError>();
        
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
            var rowNumber = 0;
            if (csv.Context.Parser != null)
            {
                rowNumber = csv.Context.Parser.Row;
            }
            var actualColumns = csv.Context.Parser.Count;
            if (actualColumns != expectedColumns)
            {
                errors.Add(new FileReaderRowError(rowNumber,
                    $"Incorrect number of columns expected: {expectedColumns}, actual: {actualColumns}",
                    csv.Context.Parser.RawRecord)
                );
                continue;
            }

            try
            {
                var record = csv.GetRecord<MeterReadRow>();
                record.RowNumber = rowNumber;
                successes.Add(record);
            }
            catch (Exception e)
            {
                errors.Add(new FileReaderRowError(rowNumber,
                    $"Encountered error {e.Message}.",
                    csv.Context.Parser.RawRecord)
                );
            }
        }
        
        return (successes, errors);
    }
}