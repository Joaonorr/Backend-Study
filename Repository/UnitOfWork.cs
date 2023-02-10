using WebApplication1.Context;

namespace WebApplication1.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ProductRepository _productRepository;
    private CategoryRepository _categoryRepository;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository 
    {
        get 
        {
            return _productRepository ??= new ProductRepository(_context); 
        }
    }
    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository ??= new CategoryRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
