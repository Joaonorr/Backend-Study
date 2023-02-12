using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Pagination;

namespace WebApplication1.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {        
    }

    public PagedList<Product> GetProductsPaged(ProductsParameters productsParameters)
    {
        return PagedList<Product>.ToPagedList(
            GetAll().OrderBy(p => p.Name),
            productsParameters.PageNumer,
            productsParameters.PageSize);
    }
}
