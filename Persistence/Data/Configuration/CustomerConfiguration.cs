using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");

            builder.Property(p => p.Id)
                .IsRequired()
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("id_Customer");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("customer_name")
                .HasMaxLength(100);

            builder.Property(p => p.Address)
                .HasColumnName("customer_address")
                .HasMaxLength(200);

            builder.Property(p => p.PhoneNumber)
                .HasColumnName("customer_phone")
                .HasMaxLength(15);

            builder.HasMany(p => p.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Angel",
                    Address = "145 QuintaSanisidro St",
                    PhoneNumber = "+1-1234460694"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Ximenita",
                    Address = "145 LaArboleda st",
                    PhoneNumber = "+1-3496504055"
                }
            );
        }
    }
}
