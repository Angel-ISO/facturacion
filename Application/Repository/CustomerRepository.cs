using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class CustomerRepository  : GenericRepository<Customer>, ICustomer
{
    public readonly FacturacionContext _Context;
    public CustomerRepository (FacturacionContext context) : base(context)
    {
        _Context = context;
    }
        public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _Context.Set<Customer>()
                                .Include(p => p.Invoices)
                                .ToListAsync();
    }
}