using WebApplication1.Models;

namespace WebApplication1.Repository.CategoryRepository;

public interface ICategoryRepository : IRepository<Category>
{
    public IEnumerable<Category> GetCategoriesProducts();
}
