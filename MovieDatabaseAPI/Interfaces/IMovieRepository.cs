using MovieDatabaseAPI.Data;
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
        Task<IEnumerable<string>> GetSearchSuggestionsAsync(string query);
        Task<int> GetRatingCountForMovieAsync(Guid movieId);
        Task<double> GetAverageRatingForMovieAsync(Guid movieId);
        Task<bool> SaveAllAsync();
    }
}
