namespace Clean.Application.Dtos;

public record NewCustomerDto(
    Guid? Id,
    string? Name,
    string? TaxNumber,
    string? Email,
    string? Phone,
    string? Address);