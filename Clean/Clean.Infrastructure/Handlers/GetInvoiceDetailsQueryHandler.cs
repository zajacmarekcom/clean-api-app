using Clean.Application.Dtos;
using Clean.Application.Queries;
using Clean.Database.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Handlers;

public class GetInvoiceDetailsQueryHandler(InvoiceDbContext context)
    : IRequestHandler<GetInvoiceDetailsQuery, InvoiceDetailsDto?>
{
    public async Task<InvoiceDetailsDto> Handle(GetInvoiceDetailsQuery request, CancellationToken cancellationToken)
    {
        var invoice = await context.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id), cancellationToken);

        if (invoice is null)
        {
            return null;
        }

        var items = invoice.Items.Select(x => new InvoiceItemDto(x.Id, x.ItemName, x.Price, x.Quantity)).ToList();
        var customer = new CustomerDto(invoice.Customer.Id, invoice.Customer.Name, invoice.Customer.TaxNumber,
            invoice.Customer.Email ?? string.Empty, invoice.Customer.Phone ?? string.Empty, invoice.Customer.Address);
        var invoiceDetails = new InvoiceDetailsDto(invoice.Id, invoice.InvoiceNumber, customer, invoice.InvoiceDate,
            invoice.Total, items);
        
        return invoiceDetails;
    }
}