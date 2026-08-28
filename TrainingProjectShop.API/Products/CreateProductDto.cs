namespace TrainingProjectShop.API.Products
{
    public record CreateProductDto(
            string Name,
            decimal Amount,
            string Currency,
            string? Description);
}
