using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Pagination;

namespace WebApplication1.Repository.CategoryRepo;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Category>> GetCategoriesProducts(CategoryParameters categoryParameters)
    {
        return await PagedList<Category>.ToPagedList(
            GetAll().Include(c => c.products).OrderBy(c => c.Name),
            categoryParameters.PageNumer,
            categoryParameters.PageSize);
    }

    public async Task<PagedList<Category>> GetCategoriesPaged(CategoryParameters categoryParameters)
    {
        return await PagedList<Category>.ToPagedList(
            GetAll().OrderBy(c => c.Name),
            categoryParameters.PageNumer,
            categoryParameters.PageSize);
    }
}
