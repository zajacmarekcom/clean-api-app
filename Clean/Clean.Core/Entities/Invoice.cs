namespace Clean.Core.Entities;

public class Invoice
{
    public Invoice(){}
    
    public Invoice(Customer customer, string invoiceNumber, DateTimeOffset invoiceDate)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        InvoiceNumber = invoiceNumber;
        InvoiceDate = invoiceDate;
    }

    public Invoice(Guid id, Customer customer, string invoiceNumber, DateTime invoiceDate,
        List<InvoiceItem> items)
    {
        Id = id;
        Customer = customer;
        InvoiceNumber = invoiceNumber;
        InvoiceDate = invoiceDate;
        _items = items;
    }

    private readonly List<InvoiceItem> _items = [];

    public Guid Id { get; set; }
    public Customer Customer { get; private set; }
    public Guid CustomerId { get; private set; }
    public string InvoiceNumber { get; private set; }
    public DateTimeOffset InvoiceDate { get; private set; }
    public IReadOnlyList<InvoiceItem> Items => _items.AsReadOnly();
    public decimal Total => _items.Sum(x => x.Price * x.Quantity);

    public void ChangeCustomer(Customer customer)
    {
        Customer = customer;
    }
    
    public void SetInvoiceDate(DateTimeOffset invoiceDate)
    {
        InvoiceDate = invoiceDate;
    }

    public void AddItem(InvoiceItem item)
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

    public void UpdateItem(InvoiceItem item)
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