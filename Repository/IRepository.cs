using System.Linq.Expressions;

namespace WebApplication1.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetAll();
    T Get(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
