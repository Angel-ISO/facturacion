using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.Property(p => p.Id)
                .IsRequired()
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("id_Product"); 

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("product_name")
                .HasMaxLength(50); 

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnName("product_price"); 

            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "arroz",
                    Price = 2400
                },
                new Product
                {
                    Id = 2,
                    Name = "leche",
                    Price = 2500
                },
                 new Product
                {
                    Id = 3,
                    Name = "jabon en polvo",
                    Price = 1500
                },
                 new Product
                {
                    Id = 4,
                    Name = "manzanaU",
                    Price = 2000
                }
            );
        }
    }
}
