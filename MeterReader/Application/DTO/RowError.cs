namespace Application.DTO;

public record RowError(
    int RowNumber,
    string ErrorMessage,
    string RawLine
);