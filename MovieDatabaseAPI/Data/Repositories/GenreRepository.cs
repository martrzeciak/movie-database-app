using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _dataContext;

        public GenreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await _dataContext.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreAsync(Guid id)
        {
            var genre = await _dataContext.Genres
                .FirstOrDefaultAsync(x => x.Id == id);

            return genre;
        }
    }
}
