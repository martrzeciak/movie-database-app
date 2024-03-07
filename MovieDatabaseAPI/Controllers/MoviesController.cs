using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _movieRepository.GetMoviesAsync();

            // if (!movies.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(movies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);

            if (movie == null) return NotFound();

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet("actor-movies/{id}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesForActor(Guid id)
        {
            var moviesForActor = await _movieRepository.GetMoviesForActorAsync(id);

            //if (!moviesForActor.Any()) return NotFound();

            return Ok(moviesForActor);
        }
    }
}
