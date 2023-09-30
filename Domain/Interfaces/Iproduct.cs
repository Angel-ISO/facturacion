using Domain.Entities;

namespace Domain.Interfaces;

public interface IProduct : IGenericRepository<Product>
{
   Task<IEnumerable<Product>> GetProductosMasCaros(int cantidad);
}