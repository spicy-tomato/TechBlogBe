using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechBlogBe.Contexts;

namespace TechBlogBe.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected ApplicationContext Context { get; }
    protected readonly IMapper Mapper;

    protected RepositoryBase(ApplicationContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public T? GetById(string id) => Context.Set<T>().Find(id);

    public IEnumerable<T> GetAll() => Context.Set<T>().AsNoTracking();

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => Context.Set<T>().Where(expression);

    public Task<List<T>> FindAsync(Expression<Func<T, bool>> expression) =>
        Context.Set<T>().Where(expression).ToListAsync();

    public T Create(T entity) => Context.Set<T>().Add(entity).Entity;

    public void CreateRange(IEnumerable<T> entities) => Context.Set<T>().AddRange(entities);

    public void Update(T entity) => Context.Set<T>().Update(entity);

    public void Delete(T entity) => Context.Set<T>().Remove(entity);

    public void DeleteRange(IEnumerable<T> entities) => Context.Set<T>().RemoveRange(entities);
}