using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
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

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            var movies = await _dataContext.Movies
                .Include(p => p.Poster)
                .Include(g => g.Genres)
                .ToListAsync();

            return movies;
        }

        public async Task<Movie?> GetMovieAsync(Guid id)
        {
            var movie = await _dataContext.Movies
                .Include(g => g.Genres)
                .Include(p => p.Poster)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesForActorAsync(Guid id)
        {
            var moviesForActor = await _dataContext.Movies
                .Where(movie => movie.Actors.Any(actor => actor.Id == id))
                .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return moviesForActor;
        }
    }
}
