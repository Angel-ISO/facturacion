using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class FacturacionContext : DbContext
{
        public FacturacionContext(DbContextOptions options) : base(options)
        {
        }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Invoice> Invoices { get; set; }
            public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

}