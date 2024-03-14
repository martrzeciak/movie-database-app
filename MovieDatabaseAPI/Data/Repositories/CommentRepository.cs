using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _dataContext;

        public CommentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Comment>> GetCommentsForMovieAsync(Guid movieId)
        {
            var comments = await _dataContext.Comments
                .Include(u => u.User)
                    .ThenInclude(i => i.UserImages)
                .Where(comment => comment.Movie.Id == movieId)
                .ToListAsync();

            return comments;
        }

        public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _dataContext.Comments
                .Include(u => u.User)
                    .ThenInclude(i => i.UserImages)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            return comment;
        }
    }
}
