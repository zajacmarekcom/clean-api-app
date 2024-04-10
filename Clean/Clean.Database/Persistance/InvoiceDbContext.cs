using Clean.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Database.Persistance;

public class InvoiceDbContext : DbContext
{
    public string DbPath { get; }
    
    public InvoiceDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "invoices.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    public required DbSet<Invoice> Invoices { get; set; }
    public required DbSet<Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InvoiceDate).IsRequired();
            entity.Property(e => e.InvoiceNumber).IsRequired();
            entity.HasOne(e => e.Customer).WithMany(c => c.Invoices).HasForeignKey(e => e.CustomerId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.TaxNumber).IsRequired();
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.Email).IsRequired(false);
            entity.Property(e => e.Phone).IsRequired(false);
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemName).IsRequired();
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.Quantity).IsRequired();
            entity.HasOne(e => e.Invoice).WithMany(i => i.Items).HasForeignKey(e => e.InvoiceId);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}