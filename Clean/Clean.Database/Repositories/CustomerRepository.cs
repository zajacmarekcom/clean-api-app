using Clean.Core.Entities;
using Clean.Core.Interfaces;
using Clean.Database.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Clean.Database.Repositories;

public class CustomerRepository(InvoiceDbContext context) : ICustomerRepository
{
    public async Task<IReadOnlyList<Customer>> ListAll()
    {
        return await context.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer?> GetById(Guid id)
    {
        return await context.Customers.AsNoTracking().FirstAsync(c => c.Id == id);
    }

    public void Add(Customer entity)
    {
        context.Customers.Add(entity);
    }

    public void Update(Customer entity)
    {
        context.Update(entity);
    }

    public void Delete(Customer entity)
    {
        context.Customers.Remove(entity);
    }

    public Task Save()
    {
        return context.SaveChangesAsync();
    }
}