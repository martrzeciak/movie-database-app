using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IRatingRepository
    {
        Task<MovieRating?> GetMovieRatingAsync(Guid userId, Guid movieId);
        Task<ActorRating?> GetActorRatingAsync(Guid userId, Guid movieId);
        Task<int> GetUserRatingForMovieAsync(Guid movieId, Guid userId);
        Task<int> GetUserRatingForActorAsync(Guid actorId, Guid userId);
        Task<int> GetRatingCountForMovieAsync(Guid movieId);
        Task<double> GetAverageRatingForMovieAsync(Guid movieId);
        Task<int> GetRatingCountForActorAsync(Guid actorId);
        Task<double> GetAverageRatingForActorAsync(Guid actorId);
        Task<IEnumerable<Movie>> GetRatedMoviesForUserAsync(Guid id);
        Task<IEnumerable<Actor>> GetRatedActorsForUserAsync(Guid id);
        void RemoveMovieRating(MovieRating movieRating);
        void RemoveActorRating(ActorRating actorRating);
    }
}