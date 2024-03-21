namespace Clean.Core.Entities;

public class InvoiceItemEntity
{
    public InvoiceItemEntity(Guid id, Guid invoiceId, string itemName, decimal price, int quantity)
    {
        Id = id;
        InvoiceId = invoiceId;
        ItemName = itemName;
        Price = price;
        Quantity = quantity;
    }
    
    public InvoiceItemEntity(Guid invoiceId, string itemName, decimal price, int quantity)
    {
        Id = Guid.NewGuid();
        InvoiceId = invoiceId;
        ItemName = itemName;
        Price = price;
        Quantity = quantity;
    }

    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
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