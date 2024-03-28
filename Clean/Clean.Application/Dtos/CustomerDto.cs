namespace Clean.Application.Dtos;

public record CustomerDto(
    Guid Id,
    string Name,
    string TaxNumber,
    string Email,
    string Phone,
    string Address);