namespace Application.DTO;

public class MeterReadRow
{
    public int AccountId { get; set; }
    public DateTime MeterReadingDateTime { get; set; }
    public string MeterReadValue { get; set; }
}