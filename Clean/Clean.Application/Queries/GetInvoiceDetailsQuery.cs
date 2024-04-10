using Clean.Application.Dtos;
using MediatR;

namespace Clean.Application.Queries;

public record GetInvoiceDetailsQuery(string Id) : IRequest<InvoiceDetailsDto?>;