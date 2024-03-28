namespace Clean.Application.Dtos;

public record InvoiceItemDto(
    Guid Id,
    decimal Quantity,
    decimal Price,
    decimal Total);