namespace Application.DTO;

public record FileReaderRowError(
    int RowNumber,
    string ErrorMessage,
    string RawLine
);