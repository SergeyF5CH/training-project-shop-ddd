using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.Infrastructure.Database.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");

            builder.Property<Guid>("Id")
                .ValueGeneratedOnAdd();

            builder.HasKey("Id");

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(x => x.Amount)
                    .HasColumnName("price_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                price.Property(x => x.Currency)
                    .HasColumnName("price_currency")
                    .IsRequired()
                    .HasMaxLength(3);
            });
        }
    }
}
