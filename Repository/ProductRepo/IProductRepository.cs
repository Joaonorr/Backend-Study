using WebApplication1.Models;
using WebApplication1.Pagination;

namespace WebApplication1.Repository.ProductRepo;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedList<Product>> GetProductsPaged(ProductsParameters productsParameters);
}
