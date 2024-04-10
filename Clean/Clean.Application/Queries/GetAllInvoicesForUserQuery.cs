using Clean.Application.Dtos;
using MediatR;

namespace Clean.Application.Queries;

public record GetAllInvoicesForUserQuery(string UserId) : IRequest<IEnumerable<InvoiceDto>>;