using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
}
