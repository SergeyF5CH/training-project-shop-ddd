using TrainingProjectShop.Domain.Customers;

namespace TrainingProjectShop.Domain.Tests;

public class CustomerTests
{
    [Fact]
    public void NewCustomer_ShouldHaveActiveStatus()
    {
        var email = new Email("pedro@example.com");

        var customers = new Customer(
            Guid.NewGuid(),
            "Pedro",
            email);

        Assert.Equal(CustomerStatus.Active, customers.Status);
    }

    [Fact]
    public void Block_ShouldChangeStatusToBlocked()
    {
        var customer = CreateCustomer();

        customer.Block();

        Assert.Equal(CustomerStatus.Blocked, customer.Status);
    }

    [Fact]
    public void Unlock_ShouldChangeStatusToActive()
    {
        var customer = CreateCustomer();

        customer.Block();
        customer.Unblocked();

        Assert.Equal(CustomerStatus.Active, customer.Status);
    }

    [Fact]
    public void Delete_ShouldChangeStatusToDelete()
    {
        var customer = CreateCustomer();

        customer.Delete();

        Assert.Equal(CustomerStatus.Deleted, customer.Status);
    }

    [Fact]
    public void Block_AlreadyBlockedCustomer_ShouldThrow()
    {
        var customer = CreateCustomer();

        customer.Block();

        Assert.Throws<InvalidOperationException>(() => customer.Block());
    }

    [Fact]
    public void Unblock_ActiveCustomer_ShouldThrow()
    {
        var customer = CreateCustomer();

        Assert.Throws<InvalidOperationException>(() => customer.Unblocked());
    }

    [Fact]
    public void DeletedCustomer_CannotBeBlocked()
    {
        var customer = CreateCustomer();
        
        customer.Delete();

        Assert.Throws<InvalidOperationException>(() => customer.Block());
    }

    private static Customer CreateCustomer()
    {
        return new Customer(
            Guid.NewGuid(),
            "Pedro",
            new Email("pedro@example.com"));
    }
}
