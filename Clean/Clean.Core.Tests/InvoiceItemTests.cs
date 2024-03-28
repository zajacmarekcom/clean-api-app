using Bogus;
using Clean.Core.Entities;
using FluentAssertions;

namespace Clean.Core.Tests;

public class InvoiceItemTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void WhenQuantityIsSetToZero_ThenThrowArgumentException()
    {
        // Arrange
        var customer = new Customer(Guid.NewGuid(), _faker.Person.FullName, _faker.Random.Int(10000, 99999).ToString(),
            _faker.Person.Email, _faker.Person.Phone, _faker.Address.FullAddress());
        var invoice = new Invoice(customer, _faker.Random.AlphaNumeric(10), _faker.Date.PastOffset());
        var invoiceItem = new InvoiceItem(invoice, _faker.Commerce.Product(), 10, 1);

        // Act
        Action act = () => invoiceItem.SetQuantity(0);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Quantity must be greater than 0*");
    }
}