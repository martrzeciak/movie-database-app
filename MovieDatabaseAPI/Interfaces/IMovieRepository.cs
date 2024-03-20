using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IMovieRepository
    {
        Task<PagedList<MovieDto>> GetMoviesAsync(MovieParams movieParams);
        Task<Movie?> GetMovieByIdAsync(Guid id);
        Task<PagedList<MovieDto>> GetMoviesForActorAsync(Guid id, PaginationParams paginationParams);
        //Task<IEnumerable<string>> GetSearchSuggestionsAsync(string query);
        Task<Movie?> GetMovieForUpdateAsync(Guid id);
        Task<IEnumerable<Movie>> GetMovieNameListAsync();
        Task<IEnumerable<Movie>> GetMovieNameListForActorAsync(Guid actorId);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
        Task<bool> SaveAllAsync();
    }
}
