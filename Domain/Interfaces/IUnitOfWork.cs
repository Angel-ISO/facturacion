namespace Domain.Interfaces;

public interface IUnitOfWork
{
    ICustomer Customers { get; }
    IInvoice Invoices { get; }
    IInvoiceDetail InvoiceDetails { get; }
    IProduct Products { get; }
    Task<int> SaveAsync();
}