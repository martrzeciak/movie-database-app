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

        public async Task<IEnumerable<Comment>> GetCommentsForMovieAsync(Guid id)
        {
            var comments = await _dataContext.Comments
                .Include(u => u.User)
                .Where(comment => comment.Movie.Id == id)
                .ToListAsync();

            return comments;
        }
    }
}
