using AutoMapper;
using TBB.Data.Models;
using TechBlogBe.Contexts;
using TechBlogBe.Repositories.Interface;

namespace TechBlogBe.Repositories.Implementations;

public class TagRepository : RepositoryBase<Tag>, ITagRepository
{
    public TagRepository(ApplicationContext context, IMapper mapper) : base(context, mapper) { }

    public void CreateRange(ICollection<string> tags)
    {
        try
        {
            CreateRange(tags.Select(tag => new Tag { Name = tag }));
        }
        catch
        {
            foreach (var tag in tags)
            {
                try
                {
                    Create(new Tag { Name = tag });
                    Context.SaveChanges();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}