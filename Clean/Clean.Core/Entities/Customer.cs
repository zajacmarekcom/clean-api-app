﻿namespace Clean.Core.Entities;

public class Customer
{
    public Customer(){}
    
    public Customer(string name, string taxNumber, string email, string phone, string address)
    {
        Id = Guid.NewGuid();
        Name = name;
        TaxNumber = taxNumber;
        Email = email;
        Phone = phone;
        Address = address;
    }
    
    public Customer(Guid id, string name, string taxNumber, string email, string phone, string address)
    {
        Id = id;
        Name = name;
        TaxNumber = taxNumber;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TaxNumber { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Address { get; set; }
    public ICollection<Invoice> Invoices { get; private set; } = new List<Invoice>();
}