using AutoMapper;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<Genre, GenreDto>();
            CreateMap<MemberUpdateDto, User>();
            CreateMap<UserImage, UserImageDto>();
            CreateMap<MovieForCreationDto, Movie>();
            CreateMap<ActorForCreationDto, Actor>();
            CreateMap<Poster, PosterDto>();
            CreateMap<ActorImage, ActorImageDto>();
            CreateMap<Review, ReviewDto>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.Likes,
                    opt => opt.MapFrom(src => src.Likes.Count()));

            CreateMap<User, MemberDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.UserImages.FirstOrDefault(p => p.IsMain).ImageUrl));

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.PosterUrl,
                    opt => opt.MapFrom(src => src.Posters.FirstOrDefault(p => p.IsMain).PosterUrl))
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
                    opt => opt.MapFrom(src => src.Images.FirstOrDefault(p => p.IsMain).ImageUrl))
                .ForMember(
                    dest => dest.AverageRating,
                    opt => opt.MapFrom(src => src.ActorRatings.Any() 
                        ? Math.Round(src.ActorRatings.Average(r => r.Rating), 1) 
                        : 0.0))
                .ForMember(
                    dest => dest.RatingCount,
                    opt => opt.MapFrom(src => src.ActorRatings.Count()));

            CreateMap<Actor, ActorNameDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Movie, MovieNameDto>();

#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
