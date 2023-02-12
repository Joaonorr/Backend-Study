using WebApplication1.Models;
using WebApplication1.Pagination;

namespace WebApplication1.Repository;

public interface IProductRepository : IRepository<Product>
{
    PagedList<Product> GetProductsPaged(ProductsParameters productsParameters);
}
