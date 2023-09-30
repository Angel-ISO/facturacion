using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class ProductRepository : GenericRepository<Product>, IProduct
{
    private readonly FacturacionContext _context;

    public ProductRepository(FacturacionContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductosMasCaros(int cantidad) =>
                    await _context.Products
                        .OrderByDescending(p => p.Price)
                        .Take(cantidad)
                        .ToListAsync();



}