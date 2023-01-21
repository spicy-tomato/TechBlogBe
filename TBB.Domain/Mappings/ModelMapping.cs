using AutoMapper;
using TBB.Data.Models;
using TBB.Data.Requests.Post;

namespace TBB.Domain.Mappings;

public class ModelMapping : Profile
{
    public ModelMapping()
    {
        CreateMap<CreatePostRequest, Post>().ForMember(
            dest => dest.Tags,
            opt => opt.MapFrom(
                src => src.Tags.Select(tag => new Tag { Name = tag })
            )
        );
    }
}