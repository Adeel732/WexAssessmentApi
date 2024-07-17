using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        var productsByCategory = data.Values.Where(p => p.Category == category);
        return Task.FromResult(productsByCategory.AsEnumerable());
    }
}
