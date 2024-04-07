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
        Task<IEnumerable<Movie>> GetSearchSuggestionsAsync(string query);
        Task<Movie?> GetMovieForUpdateAsync(Guid id);
        Task<IEnumerable<Movie>> GetMovieNameListAsync();
        Task<IEnumerable<Movie>> GetMovieNameListForActorAsync(Guid actorId);
        Task<int> GetMoviePositionAsync(Guid movieId);
        Task<Guid?> GetRandomMovieIdAsync();
        Task<IEnumerable<Movie>> GetRandomSuggestionsByGenresAsync(Guid movieId, int count);
        Task<IEnumerable<Movie>> GetUserWantToWatchMovieListAsync(Guid userId);
        Task<IEnumerable<Movie>> SearchMoviesAsync(string query);
        Task<bool> CheckIsMovieOnUserWantToWatchListAsync(Guid movieId, Guid userId);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
    }
}
