using WebApplication1.Models;

namespace WebApplication1.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<Category> GetCategoriesProducts();
}
