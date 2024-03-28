using Clean.Application.Commands;
using Clean.Core.Entities;
using Clean.Core.Interfaces;
using MediatR;

namespace Clean.Infrastructure.Handlers;

public class AddInvoiceCommandHandler(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository)
    : IRequestHandler<AddInvoiceCommand, Guid>
{
    public async Task<Guid> Handle(AddInvoiceCommand request, CancellationToken cancellationToken)
    {
        Customer customer = await GetOrCreateCustomer(request);
        Invoice invoice = new(customer, Guid.NewGuid().ToString(), request.InvoiceDate);

        var items = request.Items.Select(x => new InvoiceItem(x.Name, x.Price, x.Quantity)).ToList();
        foreach (var item in items)
        {
            invoice.AddItem(item);
        }
        invoiceRepository.Add(invoice);
        await invoiceRepository.Save();
        
        return invoice.Id;
    }

    private async Task<Customer> GetOrCreateCustomer(AddInvoiceCommand request)
    {
        if (request.Customer.Id is null)
        {
            var customer = new Customer(request.Customer.Name!, request.Customer.TaxNumber!, request.Customer.Email!,
                request.Customer.Phone!, request.Customer.Address!);
            
            customerRepository.Add(customer);
            return customer;
        }
        else
        {
            var customer = await customerRepository.GetById(request.Customer.Id!.Value);
            if (customer is null)
            {
                throw new Exception("Customer not found");
            }

            return customer;
        }
    }
}