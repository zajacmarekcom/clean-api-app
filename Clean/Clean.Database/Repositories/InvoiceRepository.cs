using Clean.Core.Entities;
using Clean.Core.Interfaces;
using Clean.Database.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Clean.Database.Repositories;

public class InvoiceRepository(InvoiceDbContext context) : IInvoiceRepository
{
    public async Task<Invoice?> GetById(Guid id)
    {
        return await context.Invoices.FindAsync(id);
    }

    public async Task<IReadOnlyList<Invoice>> ListAll()
    {
        return await context.Invoices.ToListAsync();
    }

    public void Add(Invoice entity)
    {
        context.Invoices.Add(entity);
    }

    public void Update(Invoice entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Invoice entity)
    {
        context.Invoices.Remove(entity);
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Invoice>> GetByCustomerId(Guid customerId)
    {
        var result = await context.Invoices.Where(i => i.CustomerId == customerId).ToListAsync();
        return result.AsReadOnly();
    }

    public async Task<IReadOnlyList<Invoice>> GetByDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        var result = await context.Invoices.Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate).ToListAsync();
        return result.AsReadOnly();
    }
    
    public async Task<IReadOnlyList<Invoice>> GetByDateRange(Guid customerId, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        var result = await context.Invoices.Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate).ToListAsync();
        return result.AsReadOnly();
    }
}