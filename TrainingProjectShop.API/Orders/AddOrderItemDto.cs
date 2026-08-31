using System.ComponentModel.DataAnnotations;

namespace TrainingProjectShop.API.Orders
{
    public record AddOrderItemDto(
        Guid ProductId,

        [property: Range(1, int.MaxValue)]
        int Quantity);
}
