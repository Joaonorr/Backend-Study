using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repository.ProductRepository;

public class ProductRepository : Repository<Product>, IProductRepository
{
	public ProductRepository(AppDbContext context) : base(context)
	{
	}
}
