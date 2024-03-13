using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IRatingRepository
    {
        Task<MovieRating?> GetMovieRatingAsync(Guid movieId, Guid userId);
        Task<int> GetUserRatingValueAsync(Guid movieId, Guid userId);
        Task<bool> SaveAllAsync();
    }
}
