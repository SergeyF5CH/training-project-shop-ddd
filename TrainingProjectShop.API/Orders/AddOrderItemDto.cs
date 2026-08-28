namespace TrainingProjectShop.API.Orders
{
    public record AddOrderItemDto(Guid ProductId, int Quantity);
}
