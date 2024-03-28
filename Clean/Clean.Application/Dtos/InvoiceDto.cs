namespace Clean.Application.Dtos;

public record InvoiceDto(
    Guid Id,
    string InvoiceNumber,
    string CustomerName,
    DateTimeOffset InvoiceDate,
    decimal Total);