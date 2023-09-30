using Application.Repository;
using Domain.Interfaces;
using Persistence;
namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly FacturacionContext _context;
    private ICustomer _customers;
    private IProduct _products;
    private IInvoice _invoice;
    private IInvoiceDetail _invoiceDetail;

    
    public UnitOfWork(FacturacionContext context)
    {
        _context = context;
    }

    public ICustomer Customers
    {
        get
        {
            _customers ??= new CustomerRepository(_context);
            return _customers;
        }
    }


   public IProduct Products
    {
        get
        {
            _products ??= new ProductRepository(_context);
            return _products;
        }
    }


      public IInvoice Invoices
    {
        get
        {
            _invoice ??= new InvoiceRepository(_context);
            return _invoice;
        }
    }

      public IInvoiceDetail InvoiceDetails
    {
        get
        {
            _invoiceDetail ??= new InvoiceDetailRepository(_context);
            return _invoiceDetail;
        }
    }


 
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}