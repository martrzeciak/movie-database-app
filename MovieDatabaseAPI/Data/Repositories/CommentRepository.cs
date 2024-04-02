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
                .OrderByDescending(comment => comment.CreatedAt)
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

        public void AddComment(Comment comment)
        {
            _dataContext.Comments.Add(comment);
        }

        public void Update(Comment comment)
        {
            _dataContext.Entry(comment).State = EntityState.Modified;
        }

        public void Delete(Comment comment)
        {
            _dataContext.Comments.Remove(comment);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
