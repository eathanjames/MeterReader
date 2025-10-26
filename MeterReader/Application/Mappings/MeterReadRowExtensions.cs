using Application.DTO;
using Domain.Entities;

namespace Application.Mappings;

public static class MeterReadRowExtensions
{
    public static MeterReading ToMeterReading(this MeterReadRow row)
    {
        if (row == null)
            throw new ArgumentNullException(nameof(row));

        return new MeterReading
        {
            AccountId = row.AccountId,
            MeterReadingDateTime = row.MeterReadingDateTime,
            MeterReadValue = row.MeterReadValue ?? string.Empty // handle null safely
        };
    }
}