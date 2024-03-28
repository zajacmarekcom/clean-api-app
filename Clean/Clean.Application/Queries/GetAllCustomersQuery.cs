using Clean.Application.Dtos;
using MediatR;

namespace Clean.Application.Queries;

public record GetAllCustomersQuery() : IRequest<IEnumerable<CustomerDto>>;