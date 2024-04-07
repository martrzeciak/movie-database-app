using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Review?> GetReviewByIdAsync(Guid reviewId)
        {
            var review = await _dataContext.Reviews
                .Include(u => u.User)
                    .ThenInclude(i => i.UserImages)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsForMovieAsync(Guid movieId)
        {
            var reviews = await _dataContext.Reviews
                .Include(u => u.User)
                    .ThenInclude(i => i.UserImages)
                .Include(m => m.Movie)
                    .ThenInclude(p => p.Posters)
                .Where(m => m.MovieId == movieId)
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetReviewsForUserAsync(string username)
        {
            var reviews = await _dataContext.Reviews
                .Include(u => u.User)
                    .ThenInclude(i => i.UserImages)
                .Include(m => m.Movie)
                    .ThenInclude(p => p.Posters)
                .Where(u => u.User.UserName == username)
                .ToListAsync();

            return reviews;
        }

        public void AddReview(Review review)
        {
            _dataContext.Reviews.Add(review);
        }

        public void UpdateReview(Review review)
        {
            _dataContext.Entry(review).State = EntityState.Modified;
        }

        public void RemoveReview(Review review)
        {
            _dataContext.Reviews.Remove(review);
        }
    }
}
