using TrainingProjectShop.Domain.Orders;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void NewOrder_ShouldHaveCreatedStatus()
    {
        var order = CreateOrder();

        Assert.Equal(OrderStatus.Created, order.Status);
    }

    [Fact]
    public void AddItem_ShouldAddItemToOrder()
    {
        var order = CreateOrder();
        var productId = Guid.NewGuid();
        var price = new Price(100, "USD");

        order.AddItem(productId, price, 2);

        Assert.Single(order.Items);

        var item = order.Items.First();

        Assert.Equal(productId, item.ProductId);
        Assert.Equal(price, item.Price);
        Assert.Equal(2, item.Quantity);
    }

    [Fact]
    public void AddItem_ShouldCalculateTotal()
    {
        var order = CreateOrder();

        order.AddItem(Guid.NewGuid(),
            new Price(100, "USD"),
            2);

        var item = order.Items.First();

        Assert.Equal(200, item.GetTotal());
    }

    [Fact]
    public void AddItem_ToNonCreatedOrder_ShouldThrow()
    {
        var order = CreateOrder();

        order.AddItem(Guid.NewGuid(),
            new Price(100, "USD"),
            2);

        order.Confirm();

        Assert.Throws<InvalidOperationException>(
            () => order.AddItem(
                Guid.NewGuid(),
                new Price(100, "USD"),
                2));
    }

    [Fact]
    public void Cancel_OnPaidOrder_ShouldThrow()
    {
        var order = CreateOrder();

        order.AddItem(Guid.NewGuid(),
            new Price(100, "USD"),
            2);

        order.Confirm();
        order.Pay();

        Assert.Throws<InvalidOperationException>(
            () => order.Cancel());
    }

    [Fact]
    public void CancelOrder_ShouldHaveCancelledStatus()
    {
        var order = CreateOrder();

        order.Cancel();

        Assert.Equal(OrderStatus.Canceled, order.Status);
    }

    [Fact]
    public void ConfirmOrder_ShouldHaveConfirmedStatus()
    {
        var order = CreateOrder();

        order.AddItem(Guid.NewGuid(),
            new Price(100, "USD"),
            2);

        order.Confirm();

        Assert.Equal(OrderStatus.Confirmed, order.Status);
    }

    [Fact]
    public void PaidOrder_ShouldHavePaidStatus()
    {
        var order = CreateOrder();

        order.AddItem(Guid.NewGuid(),
            new Price(100, "USD"),
            2);

        order.Confirm();
        order.Pay();

        Assert.Equal(OrderStatus.Paid, order.Status);
    }

    [Fact]
    public void Pay_OnCreatedOrder_ShouldThrow()
    {
        var order = CreateOrder();

        Assert.Throws<InvalidOperationException>(() => order.Pay());
    }

    private static Order CreateOrder()
    {
        return new Order(Guid.NewGuid(), Guid.NewGuid());
    }
}
