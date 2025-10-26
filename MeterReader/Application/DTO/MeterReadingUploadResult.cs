using Application.Mappings;

namespace Application.DTO;

public class MeterReadingUploadResult
{
    public int Total => SuccessCount + FailureCount;
    public int SuccessCount => Successful.Count;
    public int FailureCount => Failures.Count;
    public List<MeterReadRow> Successful { get; set; } = [];
    public List<RowError> Failures { get; set; } = [];
}