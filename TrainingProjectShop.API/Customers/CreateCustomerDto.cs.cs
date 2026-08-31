using System.ComponentModel.DataAnnotations;

namespace TrainingProjectShop.API.Customers
{
    public record CreateCustomerDto(
        [property: Required]
        [property: MinLength(2)]
        string Name,

        [property: Required]
        [property: EmailAddress]
        string Email);
}
