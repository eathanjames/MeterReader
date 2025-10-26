namespace Application.DTO;

public class MeterReadRow
{
    public int RowNumber { get; set; }
    public int AccountId { get; set; }
    public DateTime MeterReadingDateTime { get; set; }
    public string? MeterReadValue { get; set; }
}