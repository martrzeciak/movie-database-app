using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public MovieRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<PagedList<MovieDto>> GetMoviesAsync(MovieParams movieParams)
        {
            var query = _dataContext.Movies.AsQueryable();

            if (movieParams.ReleaseDate != -1)
            {
                query = query.Where(m => m.ReleaseDate == movieParams.ReleaseDate);
            }

            if (!string.IsNullOrEmpty(movieParams.Genre))
            {
                query = query.Where(m => m.Genres.Any(g => g.Name == movieParams.Genre));
            }

            query = movieParams.OrderBy switch
            {
                "alphabetical" => query.OrderByDescending(m => m.Title),
                _ => query.OrderByDescending(u => u.MovieRatings.Count())
            };

            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(_mapper.ConfigurationProvider),
                movieParams.PageNumber, movieParams.PageSize);
        }

        public async Task<Movie?> GetMovieAsync(Guid id)
        {
            var movie = await _dataContext.Movies
                .Include(g => g.Genres)
                .Include(p => p.Poster)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<PagedList<MovieDto>> GetMoviesForActorAsync(Guid id, PaginationParams paginationParams)
        {
            var query = _dataContext.Movies.AsQueryable();

            query.Where(movie => movie.Actors.Any(actor => actor.Id == id));

            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(_mapper.ConfigurationProvider),
                paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<IEnumerable<string>> GetSearchSuggestionsAsync(string query)
        {
            return await _dataContext.Movies
                .Where(m => m.Title.Contains(query))
                .Select(m => m.Title)
                .Distinct()
                .Take(5)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
