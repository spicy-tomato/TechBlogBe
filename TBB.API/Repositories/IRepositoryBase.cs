using System.Linq.Expressions;
using JetBrains.Annotations;

namespace TechBlogBe.Repositories;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
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