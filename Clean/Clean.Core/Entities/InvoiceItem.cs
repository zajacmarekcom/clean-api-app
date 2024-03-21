namespace Clean.Core.Entities;

public class InvoiceItem
{
    public InvoiceItem(Guid id, Invoice invoice, string itemName, decimal price, int quantity)
    {
        Id = id;
        Invoice = invoice;
        InvoiceId = invoice.Id;
        ItemName = itemName;
        Price = price;
        Quantity = quantity;
    }
    
    public InvoiceItem(Invoice invoice, string itemName, decimal price, int quantity)
    {
        Id = Guid.NewGuid();
        Invoice = invoice;
        InvoiceId = invoice.Id;
        ItemName = itemName;
        Price = price;
        Quantity = quantity;
    }

    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; private set; }
    public string ItemName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; private set; }
    
    public void SetQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
        }
        Quantity = quantity;
    }
}