using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;

namespace MovieDatabaseAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public MoviesController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _dataContext.Movies
                .Include(p => p.Poster)
                .Include(g => g.Genres)
                .ToListAsync();

            if (movies == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(movies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
        {
            var movie = await _dataContext.Movies
                .Include(g => g.Genres)
                .Include(p => p.Poster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null) return NotFound();

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet("actor-movies/{id}")]
        public async Task<ActionResult<MovieDto>> GetMoviesForActor(Guid id)
        {
            var moviesForActor = _dataContext.Movies
                .Where(movie => movie.Actors.Any(actor => actor.Id == id))
                .ProjectTo<MovieDto>(_mapper.ConfigurationProvider);

            if (moviesForActor == null)
                return NotFound();

            return Ok(moviesForActor);
        }
    }
}
