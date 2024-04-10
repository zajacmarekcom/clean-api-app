using Clean.Application.Commands;
using Clean.Application.Dtos;
using Clean.Application.Queries;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace Clean.Presentation.Grpc.Services;

public class InvoicesService(IMediator mediator) : Invoice.InvoiceBase
{
    public override async Task<InvoicesReply> GetAllInvoices(AllInvoicesRequest request, ServerCallContext context)
    {
        var query = new GetAllInvoicesForUserQuery(request.UserId);
        var invoices = await mediator.Send(query);
        
        var reply = new InvoicesReply();
        foreach (var invoice in invoices)
        {
            reply.Invoice.Add(new InvoiceReply()
            {
                Id = invoice.Id.ToString(),
                InvoiceDate = invoice.InvoiceDate.ToTimestamp(),
                CustomerName = invoice.CustomerName,
                InvoiceNumber = invoice.InvoiceNumber,
                Total = (double)invoice.Total
            });
        }
        
        return reply;
    }

    public override async Task<InvoiceDetailsReply> GetInvoiceDetails(InvoiceDetailsRequest request, ServerCallContext context)
    {
        var query = new GetInvoiceDetailsQuery(request.InvoiceId);
        var invoice = await mediator.Send(query);
        
        return new InvoiceDetailsReply()
        {
            Id = invoice.Id.ToString(),
            InvoiceDate = invoice.InvoiceDate.ToTimestamp(),
            Customer = new CustomerReply()
            {
                Id = invoice.Customer.Id.ToString(),
                Name = invoice.Customer.Name,
                TaxNumber = invoice.Customer.TaxNumber,
                Email = invoice.Customer.Email,
                Phone = invoice.Customer.Phone,
                Address = invoice.Customer.Address
            },
            InvoiceNumber = invoice.InvoiceNumber,
            Total = (double)invoice.Total,
            Items = {invoice.Items.Select(i => new InvoiceItemReply()
            {
                Id = i.Id.ToString(),
                ItemName = i.ItemName,
                Quantity = (double)i.Quantity,
                Price = (double)i.Price,
                Total = (double)i.Total
            })}
        };
    }

    public override async Task<CustomersReply> GetCustomers(empty request, ServerCallContext context)
    {
        var query = new GetAllCustomersQuery();
        var customers = await mediator.Send(query);
        
        var reply = new CustomersReply();
        foreach (var customer in customers)
        {
            reply.Customer.Add(new CustomerReply()
            {
                Id = customer.Id.ToString(),
                Name = customer.Name,
                TaxNumber = customer.TaxNumber,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            });
        }
        
        return reply;
    }

    public override async Task<NewInvoiceReply> CreateInvoice(CreateInvoiceRequest request, ServerCallContext context)
    {
        var customerDto = new NewCustomerDto(
            request.Customer.Id is null ? null : Guid.Parse(request.Customer.Id),
            request.Customer.Name,
            request.Customer.TaxNumber,
            request.Customer.Email,
            request.Customer.Phone,
            request.Customer.Address);
        var items = request.Items.Select(i => new NewInvoiceItemDto(
            i.ItemName,
            (decimal)i.Price,
            i.Quantity)).ToList();
        
        var command = new AddInvoiceCommand(request.UserId, request.InvoiceDate.ToDateTime(), customerDto, items);
        
        var result = await mediator.Send(command);
        
        return new NewInvoiceReply()
        {
            Id = result.ToString()
        };
    }
}