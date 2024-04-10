using Clean.Application.Dtos;

namespace Clean.GrpcClient;

public record AddInvoiceRequest(string UserId, DateTimeOffset InvoiceDate, NewCustomerDto Customer, List<NewInvoiceItemDto> Items);