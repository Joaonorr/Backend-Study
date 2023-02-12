using WebApplication1.Models;
using WebApplication1.Pagination;

namespace WebApplication1.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    PagedList<Category> GetCategoriesProducts(CategoryParameters categoryParameters);
    PagedList<Category> GetCategoriesPaged(CategoryParameters categoryParameters);
}
