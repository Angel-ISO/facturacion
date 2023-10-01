using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class InvoiceRepository  : GenericRepository<Invoice>, IInvoice
{
    public readonly FacturacionContext _Context;
    public InvoiceRepository (FacturacionContext context) : base(context)
    {
        _Context = context;
    }

    
       public override async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _Context.Set<Invoice>()
                                .Include(p => p.Details)
                                .Include(p => p.Customer)
                                .ToListAsync();
    }

      public async Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId)
    {
        return await _Context.Set<Invoice>()
            .Where(i => i.CustomerId == customerId)
            .Include(i => i.Details)
            .ThenInclude(d => d.Product)
            .ToListAsync();
    }





}