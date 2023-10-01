using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class InvoiceDetailRepository  : GenericRepository<InvoiceDetail>, IInvoiceDetail
{
    public readonly  FacturacionContext _Context;
    public InvoiceDetailRepository (FacturacionContext context) : base(context)
    {
        _Context = context;
    }

       public override async Task<IEnumerable<InvoiceDetail>> GetAllAsync()
    {
        return await _Context.Set<InvoiceDetail>()
                                .Include(p => p.Product)
                                .Include(p => p.Invoice)
                                .ToListAsync();
    }

/* public async Task<IEnumerable<InvoiceDetail>> GetByInvoiceIdAsync(int invoiceId)
    {
        return await _Context.Set<InvoiceDetail>()
            .Include(p => p.Product)
            .Include(p => p.Invoice)
            .Where(detail => detail.InvoiceId == invoiceId)
            .ToListAsync();
            
             }
            */
    }



