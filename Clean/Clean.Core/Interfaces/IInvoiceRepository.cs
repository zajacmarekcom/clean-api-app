using Clean.Core.Entities;

namespace Clean.Core.Interfaces;

public interface IInvoiceRepository : IRepository<InvoiceEntity>
{
    Task<IReadOnlyList<InvoiceEntity>> GetByCustomerId(Guid customerId);
    Task<IReadOnlyList<InvoiceEntity>> GetByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);
}