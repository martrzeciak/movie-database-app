using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsForMovieAsync(Guid id);
        Task<Comment?> GetCommentByIdAsync(Guid commentId);
        Task<bool> CheckIfCommentIsLikedByUser(Guid userId, Guid commentId);
        void AddComment(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}
