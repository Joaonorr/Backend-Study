using WebApplication1.Repository.CategoryRepo;
using WebApplication1.Repository.ProductRepo;

namespace WebApplication1.Repository.UnitWork;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task Commit();
}
