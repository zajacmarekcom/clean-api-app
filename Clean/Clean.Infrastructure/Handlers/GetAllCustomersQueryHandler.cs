using Clean.Application.Dtos;
using Clean.Application.Queries;
using Clean.Database.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Handlers;

public class GetAllCustomersQueryHandler(InvoiceDbContext context)
    : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await context.Customers
            .Select(x => new CustomerDto(x.Id, x.Name, x.TaxNumber, x.Email, x.Phone, x.Address)).ToListAsync();
        
        return customers;
    }
}