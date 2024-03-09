using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IMovieRepository
    {
        Task<PagedList<MovieDto>> GetMoviesAsync(MovieParams movieParams);
        Task<Movie?> GetMovieAsync(Guid id);
        Task<PagedList<MovieDto>> GetMoviesForActorAsync(Guid id, PaginationParams paginationParams);
    }
}
