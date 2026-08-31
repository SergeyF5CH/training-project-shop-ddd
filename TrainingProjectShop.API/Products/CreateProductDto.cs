using System.ComponentModel.DataAnnotations;

namespace TrainingProjectShop.API.Products
{
    public record CreateProductDto(
            [property: Required]
            [property: MinLength(2)]
            string Name,

            [property: Range(0.01, double.MaxValue)]
            decimal Amount,

            [property: Required]
            [property: StringLength(3, MinimumLength = 3)]
            string Currency,

            string? Description);
}
