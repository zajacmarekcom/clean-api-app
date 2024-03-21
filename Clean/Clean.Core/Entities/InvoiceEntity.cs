namespace Clean.Core.Entities;

public class InvoiceEntity
{
    public InvoiceEntity(Guid customerId, string invoiceNumber, DateTime invoiceDate)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        InvoiceNumber = invoiceNumber;
        InvoiceDate = invoiceDate;
    }

    public InvoiceEntity(Guid id, Guid customerId, string invoiceNumber, DateTime invoiceDate,
        List<InvoiceItemEntity> items)
    {
        Id = id;
        CustomerId = customerId;
        InvoiceNumber = invoiceNumber;
        InvoiceDate = invoiceDate;
        _items = items;
    }

    private readonly List<InvoiceItemEntity> _items = [];

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string InvoiceNumber { get; private set; }
    public DateTimeOffset InvoiceDate { get; private set; }
    public IReadOnlyList<InvoiceItemEntity> Items => _items.AsReadOnly();
    public decimal Total => _items.Sum(x => x.Price * x.Quantity);

    public void ChangeCustomer(CustomerEntity customer)
    {
        CustomerId = customer.Id;
    }
    
    public void SetInvoiceDate(DateTimeOffset invoiceDate)
    {
        InvoiceDate = invoiceDate;
    }

    public void AddItem(InvoiceItemEntity item)
    {
        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void UpdateItem(InvoiceItemEntity item)
    {
        var existingItem = _items.FirstOrDefault(x => x.Id == item.Id);
        if (existingItem != null)
        {
            existingItem.ItemName = item.ItemName;
            existingItem.Price = item.Price;
            existingItem.SetQuantity(item.Quantity);
        }
    }
}