using Domain.Entities;

namespace Domain.Interfaces;

public interface IInvoiceDetail : IGenericRepository<InvoiceDetail>
{
    //Task<IEnumerable<InvoiceDetail>> GetByInvoiceIdAsync(int invoiceId);
}