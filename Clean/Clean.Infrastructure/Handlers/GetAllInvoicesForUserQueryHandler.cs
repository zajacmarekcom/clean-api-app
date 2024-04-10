using Clean.Application.Dtos;
using Clean.Application.Queries;
using Clean.Database.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Handlers;

public class GetAllInvoicesForUserQueryHandler(InvoiceDbContext context)
    : IRequestHandler<GetAllInvoicesForUserQuery, IEnumerable<InvoiceDto>>
{
    public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesForUserQuery request,
        CancellationToken cancellationToken)
    {
        var invoices = await context.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Items)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new InvoiceDto(x.Id, x.InvoiceNumber, x.Customer.Name, x.InvoiceDate, x.Total)).ToListAsync();

        return invoices;
    }
}