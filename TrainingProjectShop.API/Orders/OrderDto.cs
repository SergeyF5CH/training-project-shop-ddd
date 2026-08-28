using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.API.Orders
{
    public record OrderDto(
        Guid id,
        Guid CustomerId,
        OrderStatus Status,
        IReadOnlyCollection<OrderItemDto> Items);
}
