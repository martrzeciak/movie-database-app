using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Extensions;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IRatingRepository ratingRepository,
            IGenreRepository genreRepository, IActorRepository actorRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _ratingRepository = ratingRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MovieDto>>> GetMovies([FromQuery] MovieParams movieParams)
        {
            var movies = await _movieRepository.GetMoviesAsync(movieParams);

            Response.AddPaginationHeader(new PaginationHeader(
                movies.CurrentPage, movies.PageSize, movies.TotalCount, movies.TotalPages));

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);

            if (movie == null) return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movie);

            movieDto.AverageRating = await _ratingRepository.GetAverageRatingForMovieAsync(id);
            movieDto.RatingCount = await _ratingRepository.GetRatingCountForMovieAsync(id);

            return Ok(movieDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MovieDto>> AddMovie(MovieForCreationDto movieForCreationDto)
        {
            var movie = _mapper.Map<Movie>(movieForCreationDto);

            if (movieForCreationDto.GenreIds.Count != 0)
            {
                foreach (var genreId in movieForCreationDto.GenreIds)
                {
                    var genre = await _genreRepository.GetGenreByIdAsync(genreId);
                    if (genre != null)
                        movie.Genres.Add(genre);
                }
            }


            if (movieForCreationDto.ActorIds.Count != 0)
            {
                foreach (var actorId in movieForCreationDto.ActorIds)
                {
                    var actor = await _actorRepository.GetActorAsync(actorId);
                    if (actor != null)
                        movie.Actors.Add(actor);
                }
            }

            _movieRepository.Add(movie);
            var movieDto = _mapper.Map<MovieDto>(movie);

            if (await _movieRepository.SaveAllAsync()) 
                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movieDto);

            return BadRequest("Failed to add movie");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDto>> UpdateMovie(Guid id, MovieForCreationDto movieForUpdateDto)
        {
            var movie = await _movieRepository.GetMovieForUpdateAsync(id);

            if (movie == null) return NotFound();

            _mapper.Map(movieForUpdateDto, movie);

            movie.Genres.Clear();
            if (movieForUpdateDto.GenreIds.Count != 0)
            {
                foreach (var genreId in movieForUpdateDto.GenreIds)
                {
                    var genre = await _genreRepository.GetGenreByIdAsync(genreId);
                    if (genre != null)
                        movie.Genres.Add(genre);
                }
            }

            movie.Actors.Clear();
            if (movieForUpdateDto.ActorIds.Count != 0)
            {
                foreach (var actorId in movieForUpdateDto.ActorIds)
                {
                    var actor = await _actorRepository.GetActorAsync(actorId);
                    if (actor != null)
                        movie.Actors.Add(actor);
                }
            }

            _movieRepository.Update(movie);

            if (await _movieRepository.SaveAllAsync())
            {
                var updatedMovieDto = _mapper.Map<MovieDto>(movie);
                return Ok(updatedMovieDto);
            }

            return BadRequest("Failed to update movie");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(Guid id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);

            if (movie == null) return NotFound();

            _movieRepository.Delete(movie);

            if (await _movieRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Failed to delete movie");
        }

        [HttpGet("actor-movies/{id}")]
        public async Task<ActionResult<PagedList<MovieDto>>> GetMoviesForActor(Guid id,
            [FromQuery] PaginationParams paginationParams)
        {
            var moviesForActor = await _movieRepository.GetMoviesForActorAsync(id, paginationParams);

            Response.AddPaginationHeader(new PaginationHeader(
                moviesForActor.CurrentPage,
                moviesForActor.PageSize,
                moviesForActor.TotalCount,
                moviesForActor.TotalPages));

            return Ok(moviesForActor);
        }

        //[HttpGet("search-suggestions")]
        //public async Task<ActionResult<IEnumerable<string>>> GetSearchSuggestions([FromQuery] string query)
        //{
        //    var suggestions = await _movieRepository.GetSearchSuggestionsAsync(query);

        //    return Ok(suggestions);
        //}

        [HttpGet("movie-name")]
        public async Task<ActionResult<IEnumerable<MovieNameDto>>> GetMovieNameList()
        {
            var movies = await _movieRepository.GetMovieNameListAsync();

            return Ok(_mapper.Map<IEnumerable<MovieNameDto>>(movies));
        }

        [HttpGet("movie-name/{actorId}")]
        public async Task<ActionResult<IEnumerable<MovieNameDto>>> GetMovieNameListForActor(Guid actorId)
        {
            var movies = await _movieRepository.GetMovieNameListForActorAsync(actorId);

            return Ok(_mapper.Map<IEnumerable<MovieNameDto>>(movies));
        }
    }
}
