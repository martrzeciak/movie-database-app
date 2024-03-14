using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _dataContext;

        public RatingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<MovieRating?> GetMovieRatingAsync(Guid userId, Guid movieId)
        {
            return await _dataContext.MovieRatings.FindAsync(userId, movieId);
        }

        public async Task<ActorRating?> GetActorRatingAsync(Guid userId, Guid movieId)
        {
            return await _dataContext.ActorRatings.FindAsync(userId, movieId);
        }

        public async Task<int> GetUserRatingForMovieAsync(Guid movieId, Guid userId)
        {
            var userRating = await _dataContext.MovieRatings
                .Where(r => r.MovieId == movieId && r.UserId == userId)
                .Select(r => r.Rating)
                .FirstOrDefaultAsync();

            return userRating;
        }

        public async Task<int> GetUserRatingForActorAsync(Guid actorId, Guid userId)
        {
            var actorRating = await _dataContext.ActorRatings
                .Where(r => r.ActorId == actorId && r.UserId == userId)
                .Select(r => r.Rating)
                .FirstOrDefaultAsync();

            return actorRating;
        }

        public async Task<int> GetRatingCountForMovieAsync(Guid movieId)
        {
            return await _dataContext.MovieRatings
                .Where(r => r.MovieId == movieId)
                .CountAsync();
        }

        public async Task<int> GetRatingCountForActorAsync(Guid actorId)
        {
            return await _dataContext.ActorRatings
                            .Where(r => r.ActorId == actorId)
                            .CountAsync();
        }

        public async Task<double> GetAverageRatingForMovieAsync(Guid movieId)
        {
            var averageRating = await _dataContext.MovieRatings
                .Where(r => r.MovieId == movieId)
                .Select(r => (double)r.Rating)
                .DefaultIfEmpty()
                .AverageAsync();

            return Math.Round(averageRating, 1);
        }

        public async Task<double> GetAverageRatingForActorAsync(Guid actorId)
        {
            var averageRating = await _dataContext.ActorRatings
                .Where(r => r.ActorId == actorId)
                .Select(r => (double)r.Rating)
                .DefaultIfEmpty()
                .AverageAsync();

            return Math.Round(averageRating, 1);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
