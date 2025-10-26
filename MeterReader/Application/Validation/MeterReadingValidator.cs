using System.Text.RegularExpressions;
using Application.DTO;
using Application.Interfaces;

namespace Application.Validation;

public class MeterReadingValidator(IAccountRepository accountRepository, IMeterReadingRepository meterReadingRepository): IMeterReadingValidator
{
    public async Task<(bool success, List<string> Errors)> ValidateAsync(MeterReadRow row)
    {
        var errors = new List<string>();
        
        if (!await IsValidAccountAsync(row.AccountId))
        {
            errors.Add("Account does not exist");
        }
        
        if(!IsValidMeterReading(row.MeterReadValue))
        {
            errors.Add("Meter read value is not valid");
        }

        if (await IsDuplicateAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue ?? ""))
        {
            errors.Add("Meter reading already exists");
        }
        if (await IsOlderEntryAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue ?? ""))
        {
            errors.Add("Meter reading is older than existing entries");
        }


        return(errors.Count == 0, errors);
    }

    private async Task<bool> IsValidAccountAsync(int accountId)
    {
        return await accountRepository.ExistsAsync(accountId);
    }
    

    private static bool IsValidMeterReading(string? meterReading)
    {
        return string.IsNullOrWhiteSpace(meterReading) || Regex.IsMatch(meterReading, @"^\d{5}$");
    }

    private async Task<bool> IsDuplicateAsync(int accountId, DateTime date, string meterReading)
    {
        return await meterReadingRepository.ExistsAsync(accountId, date, meterReading);
    }
    
    private async Task<bool> IsOlderEntryAsync(int accountId, DateTime date, string meterReading)
    {
        return await meterReadingRepository.NewerExistsAsync(accountId, date, meterReading);
    }
}