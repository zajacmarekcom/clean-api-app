using Clean.Application.Dtos;
using MediatR;

namespace Clean.Application.Commands;

public record AddInvoiceCommand(DateTimeOffset InvoiceDate, NewCustomerDto Customer, List<NewInvoiceItemDto> Items)
    : IRequest<Guid>;