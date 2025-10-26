using Application.DTO;
using CsvHelper.Configuration;

namespace Application.Mappings;

public sealed class MeterReadCsvMap : ClassMap<MeterReadRow>
{
    public MeterReadCsvMap()
    {
        Map(m => m.AccountId).Name("AccountId");
        Map(m => m.MeterReadingDateTime).Name("MeterReadingDateTime")
            .TypeConverterOption.Format("dd/MM/yyyy HH:mm");
        Map(m => m.MeterReadValue).Name("MeterReadValue");
    }
}