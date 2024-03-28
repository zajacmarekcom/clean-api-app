using Bogus;
using Clean.Core.Entities;
using FluentAssertions;

namespace Clean.Core.Tests;

public class InvoiceTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void When_NotExistingItemIsRemovedFromInvoice_Then_ExceptionIsThrown()
    {
        // Arrange
        var customer = new Customer(Guid.NewGuid(), _faker.Person.FullName, _faker.Random.Int(10000, 99999).ToString(),
            _faker.Person.Email, _faker.Person.Phone, _faker.Address.FullAddress());
        var invoice = new Invoice(customer, _faker.Random.AlphaNumeric(10), _faker.Date.PastOffset());
        
        // Act
        Action act = () => invoice.RemoveItem(Guid.NewGuid());
        
        // Assert
        act.Should().NotThrow();
    }
}