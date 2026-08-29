using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProjectShop.Domain.Customers;

namespace TrainingProjectShop.Infrastructure.Database.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) 
        {
            builder.ToTable("customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Value)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(320);
            });
        }
    }
}
