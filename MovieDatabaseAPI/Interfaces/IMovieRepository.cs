using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie?> GetMovieAsync(Guid id);
        Task<IEnumerable<MovieDto>> GetMoviesForActorAsync(Guid id);
    }
}
