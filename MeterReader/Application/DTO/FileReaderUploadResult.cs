using Application.Mappings;

namespace Application.DTO;

public class FileReaderUploadResult<T>
{
    public int Total => SuccessCount + FailureCount;
    private int SuccessCount => Successful.Count;
    private int FailureCount => Failures.Count;
    public List<T> Successful { get; set; } = [];
    public List<FileReaderRowError> Failures { get; set; } = [];
}