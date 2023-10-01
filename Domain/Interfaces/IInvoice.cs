using Domain.Entities;

namespace Domain.Interfaces;

public interface IInvoice : IGenericRepository<Invoice>
{
 Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId);
}