using AutoMapper;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap<User, UserDto>();
            CreateMap<User, MemberDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.UserImages.FirstOrDefault(p => p.IsMain).ImageUrl));
            CreateMap<RegisterDto, User>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<MemberUpdateDto, User>();
            CreateMap<UserImage, UserImageDto>();
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.PosterUrl,
                    opt => opt.MapFrom(src => src.Poster.PosterUrl));
            CreateMap<Actor, ActorDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ActorImage.ImageUrl));
        }
    }
}
