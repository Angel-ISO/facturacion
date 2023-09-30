using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoices"); 

            builder.Property(p => p.Id)
                .IsRequired()
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("id_Invoice"); 

            builder.Property(p => p.Date)
                .IsRequired()
                .HasColumnName("invoice_date"); 

            builder.Property(p => p.CustomerId)
                .IsRequired()
                .HasColumnName("customer_id"); 

            builder.Property(p => p.Total)
                .IsRequired()
                .HasColumnName("total");  

            builder.HasOne(p => p.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); //al usar esta propiedad me aseguro que cuando se elimine esta foranea los datos relacionados se eliminaran en cascada

            builder.HasMany(p => p.Details)
                .WithOne(d => d.Invoice)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasData(
                new Invoice
                {
                    Id = 1,
                    Date = new DateTime(2023, 9, 30),
                    CustomerId = 1,
                    Total = 650
                },
                new Invoice
                {
                    Id = 2,
                    Date = new DateTime(2023, 9, 29),
                    CustomerId = 2,
                    Total = 950
                },
                new Invoice
                {
                    Id = 3,
                    Date = new DateTime(2023, 9, 28),
                    CustomerId = 2,
                    Total = 750
                },
                new Invoice
                {
                    Id = 4,
                    Date = new DateTime(2023, 9, 27),
                    CustomerId = 1,
                    Total = 550
                },
                new Invoice
                {
                    Id = 5,
                    Date = new DateTime(2023, 9, 26),
                    CustomerId = 2,
                    Total = 850
                }
            );
        }
    }
}
