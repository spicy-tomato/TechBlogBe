using AutoMapper;
using TBB.Data.Dtos.User;
using TBB.Data.Models;
using TBB.Data.Requests.Post;
using TBB.Data.Response.User;

namespace TBB.Domain.Mappings;

public class ModelMapping : Profile
{
    public ModelMapping()
    {
        #region Post

        CreateMap<CreatePostRequest, Post>().ForMember(
            dest => dest.Tags,
            opt => opt.MapFrom(
                src => src.Tags.Select(tag => new Tag { Name = tag })
            )
        );

        CreateMap<Post, GetPostResponse>().ForMember(
            dest => dest.Tags,
            opt => opt.MapFrom(
                src => src.Tags.Select(tag => tag.Name)
            )
        );

        #endregion

        #region User

        CreateMap<User, SimpleUserDto>();
        CreateMap<SignUpResponse, User>();

        #endregion
    }
}