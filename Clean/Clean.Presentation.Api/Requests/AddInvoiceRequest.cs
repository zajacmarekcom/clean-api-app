using Clean.Application.Dtos;

namespace Clean.Presentation.Api.Requests;

public record AddInvoiceRequest(DateTimeOffset InvoiceDate, NewCustomerDto Customer, List<NewInvoiceItemDto> Items);