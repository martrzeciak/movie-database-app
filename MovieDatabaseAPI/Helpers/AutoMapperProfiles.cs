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
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieDetailDto>();
            CreateMap<Genre, GenreDto>();
        }
    }
}
