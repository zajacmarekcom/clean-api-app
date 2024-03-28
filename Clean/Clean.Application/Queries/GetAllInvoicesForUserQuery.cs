using Clean.Application.Dtos;
using MediatR;

namespace Clean.Application.Queries;

public record GetAllInvoicesForUserQuery(Guid UserId) : IRequest<IEnumerable<InvoiceDto>>;