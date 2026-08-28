using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.API.Orders
{
    public record OrderItemDto(
        Guid ProductId,
        decimal Amount, 
        string Currency,
        int Quantity,
        decimal Total);
}
