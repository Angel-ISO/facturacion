using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("invoice_details"); 

            builder.Property(p => p.Id)
                .IsRequired()
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("id_invDetail"); 

            builder.Property(p => p.InvoiceId)
                .IsRequired()
                .HasColumnName("invoice_id"); 

            builder.Property(p => p.ProductId)
                .IsRequired()
                .HasColumnName("product_id"); 

            builder.Property(p => p.Quantity)
                .IsRequired()
                .HasColumnName("quantity"); 

            builder.Property(p => p.Subtotal)
                .IsRequired()
                .HasColumnName("subtotal"); 

            builder.HasOne(p => p.Invoice)
                .WithMany(i => i.Details)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            builder.HasData(
                new InvoiceDetail
                {
                    Id = 1,
                    InvoiceId = 1,
                    ProductId = 1,
                    Quantity = 2,
                    Subtotal = 200
                },
                new InvoiceDetail
                {
                    Id = 2,
                    InvoiceId = 1,
                    ProductId = 2,
                    Quantity = 3,
                    Subtotal = 300
                },
                 new InvoiceDetail
                 {
                     Id = 3,
                     InvoiceId = 2,
                     ProductId = 1,
                     Quantity = 4,
                     Subtotal = 400
                            },
                new InvoiceDetail
                {
                    Id = 4,
                    InvoiceId = 2,
                    ProductId = 3,
                    Quantity = 2,
                    Subtotal = 300
                },
                new InvoiceDetail
                {
                    Id = 5,
                    InvoiceId = 3,
                    ProductId = 2,
                    Quantity = 5,
                    Subtotal = 750
                }
            );
        }
    }
}
