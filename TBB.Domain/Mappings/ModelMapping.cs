using AutoMapper;
using TBB.Data.Dtos.Post;
using TBB.Data.Dtos.User;
using TBB.Data.Models;
using TBB.Data.Requests.Post;
using TBB.Data.Requests.User;
using TBB.Data.Response.User;

namespace TBB.Domain.Mappings;

public class ModelMapping : Profile
{
    public ModelMapping()
    {
        #region Post

        CreateMap<CreatePostRequest, Post>()
           .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(
                    src => src.Tags.Select(tag => new Tag { Name = tag })
                )
            );

        CreateMap<Post, GetPostResponse>()
           .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(
                    src => src.Tags.Select(tag => tag.Name)
                )
            ).ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => src.User)
            );

        CreateMap<Post, PostSummary>()
           .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(
                    src => src.Tags.Select(tag => tag.Name)
                )
            ).ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.User)
            );

        #endregion

        #region User

        CreateMap<User, SimpleUserDto>();
        CreateMap<User, UserSummary>()
           .ForMember(dest => dest.JoinedDate,
                opt => opt.MapFrom(src => src.CreatedAt)
            );
        CreateMap<SignUpRequest, User>();
        CreateMap<SignUpResponse, User>();

        #endregion
    }
}