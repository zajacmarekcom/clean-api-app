namespace Clean.Application.Dtos;

public record InvoiceDetailsDto(
    Guid Id,
    string InvoiceNumber,
    CustomerDto Customer,
    DateTimeOffset InvoiceDate,
    decimal Total,
    IReadOnlyList<InvoiceItemDto> Items);