using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repository.CategoryRepository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
	public CategoryRepository(AppDbContext context) : base(context)
	{			
	}

	public IEnumerable<Category> GetCategoriesProducts()
	{
		return GetAll().Include(c => c.products);
	}
}
