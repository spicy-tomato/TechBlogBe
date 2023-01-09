using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TechBlogBe.Contexts;

namespace TechBlogBe.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected ApplicationContext Context { get; }

    protected RepositoryBase(ApplicationContext context)
    {
        Context = context;
    }

    public T? GetById(string id) => Context.Set<T>().Find(id);

    public IEnumerable<T> GetAll() => Context.Set<T>().AsNoTracking();

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => Context.Set<T>().Where(expression);

    public T Create(T entity) => Context.Set<T>().Add(entity).Entity;

    public void CreateRange(IEnumerable<T> entities) => Context.Set<T>().AddRange(entities);

    public void Update(T entity) => Context.Set<T>().Update(entity);

    public void Delete(T entity) => Context.Set<T>().Remove(entity);

    public void DeleteRange(IEnumerable<T> entities) => Context.Set<T>().RemoveRange(entities);
}