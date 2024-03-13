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

        public async Task<MovieRating?> GetMovieRatingAsync(Guid movieId, Guid userId)
        {
            var rating = await _dataContext.MovieRatings.FindAsync(userId, movieId);
            return rating;
        }

        public async Task<int> GetUserRatingValueAsync(Guid movieId, Guid userId)
        {
            var userRating = await _dataContext.MovieRatings
                .Where(r => r.MovieId == movieId && r.UserId == userId)
                .Select(r => r.Rating)
                .FirstOrDefaultAsync();

            return userRating;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
