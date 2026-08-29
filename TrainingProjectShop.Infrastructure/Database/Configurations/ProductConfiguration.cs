using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Infrastructure.Database.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(200);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(x => x.Amount)
                    .HasColumnName("price_amount")
                    .IsRequired();

                price.Property(x => x.Currency)
                    .HasColumnName("price_currency")
                    .IsRequired()
                    .HasMaxLength(3);
            });
        }
    }
}
