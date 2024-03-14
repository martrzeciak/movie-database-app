using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsForMovieAsync(Guid id);
        Task<Comment?> GetCommentByIdAsync(Guid commentId);
    }
}
