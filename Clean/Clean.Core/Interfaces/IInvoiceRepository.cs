using Clean.Core.Entities;

namespace Clean.Core.Interfaces;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<IReadOnlyList<Invoice>> GetByCustomerId(Guid customerId);
    Task<IReadOnlyList<Invoice>> GetByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);
    Task<IReadOnlyList<Invoice>> GetByDateRange(Guid customerId, DateTimeOffset startDate, DateTimeOffset endDate);
}