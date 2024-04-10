namespace Clean.Application.Dtos;

public record InvoiceItemDto(
    Guid Id,
    string ItemName,
    decimal Quantity,
    decimal Price)
{
    public decimal Total => Quantity * Price;
}