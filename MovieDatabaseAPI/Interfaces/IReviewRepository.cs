using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetReviewByIdAsync(Guid reviewId);
        Task<IEnumerable<Review>> GetReviewsForMovieAsync(Guid movieId);
        Task<IEnumerable<Review>> GetReviewsForUserAsync(string username);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void RemoveReview(Review review);
    }
}