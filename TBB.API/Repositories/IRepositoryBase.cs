using System.Linq.Expressions;

namespace TechBlogBe.Repositories;

public interface IRepositoryBase<T>
{
    T? GetById(string id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    T? Create(T entity);
    void CreateRange(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}