using Application.DTO;

namespace Application.Interfaces;

public interface IMeterReadingValidator
{
    Task<(bool success, List<string> Errors)> ValidateAsync(MeterReadRow row);
}