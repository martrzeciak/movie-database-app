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
            
            CreateMap<RegisterDto, User>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<MemberUpdateDto, User>();
            CreateMap<UserImage, UserImageDto>();
            CreateMap<User, MemberDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.UserImages.FirstOrDefault(p => p.IsMain).ImageUrl));
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.PosterUrl,
                    opt => opt.MapFrom(src => src.Poster.PosterUrl))
                .ForMember(
                    dest => dest.AverageRating,
                    opt => opt.MapFrom(src => src.MovieRatings.Any() 
                        ? Math.Round(src.MovieRatings.Average(r => r.Rating), 1) 
                        : 0.0))
                .ForMember(
                    dest => dest.RatingCount,
                    opt => opt.MapFrom(src => src.MovieRatings.Count()));
            CreateMap<Actor, ActorDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ActorImage.ImageUrl))
                .ForMember(
                    dest => dest.AverageRating,
                    opt => opt.MapFrom(src => src.ActorRatings.Any() 
                        ? Math.Round(src.ActorRatings.Average(r => r.Rating), 1) 
                        : 0.0))
                .ForMember(
                    dest => dest.RatingCount,
                    opt => opt.MapFrom(src => src.ActorRatings.Count()));
        }
    }
}
