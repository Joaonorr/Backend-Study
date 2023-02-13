using WebApplication1.Models;
using WebApplication1.Pagination;

namespace WebApplication1.Repository.CategoryRepo;

public interface ICategoryRepository : IRepository<Category>
{
    Task<PagedList<Category>> GetCategoriesProducts(CategoryParameters categoryParameters);
    Task<PagedList<Category>> GetCategoriesPaged(CategoryParameters categoryParameters);
}
