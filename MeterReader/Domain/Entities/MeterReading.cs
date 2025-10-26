using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class MeterReading
{
    public required int AccountId { get; set; }
    public required DateTime MeterReadingDateTime { get; set; }
    public required string MeterReadValue { get; set; }
}