using API.Extensions;
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
        private readonly IPhotoService _photoService;

        public MoviesController(IMovieRepository movieRepository, IRatingRepository ratingRepository,
            IGenreRepository genreRepository, IActorRepository actorRepository, IMapper mapper,
            IPhotoService photoService)
        {
            _movieRepository = movieRepository;
            _ratingRepository = ratingRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
            _photoService = photoService;
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
            movieDto.MoviePosition = await _movieRepository.GetMoviePositionAsync(movie.Id);

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

        [HttpGet("search-suggestions")]
        public async Task<ActionResult<IEnumerable<MovieNameDto>>> GetSearchSuggestions([FromQuery] string query)
        {
            var suggestions = await _movieRepository.GetSearchSuggestionsAsync(query);

            return Ok(_mapper.Map<IEnumerable<MovieNameDto>>(suggestions));
        }

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

        [Authorize]
        [HttpPost("add-movie-poster/{movieId}")]
        public async Task<ActionResult<UserImageDto>> UpdateUserPhoto(Guid movieId, IFormFile file)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound();

            var results = await _photoService.AddPhotoAsync(file);

            if (results.Error != null) return BadRequest(results.Error.Message);

            var poster = new Poster
            {
                PosterUrl = results.SecureUrl.AbsoluteUri,
                PublicId = results.PublicId
            };

            if (movie.Posters.Count == 0) poster.IsMain = true;

            movie.Posters.Add(poster);

            if (await _movieRepository.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetMovie),
                    new { id = movie.Id },
                    _mapper.Map<PosterDto>(poster));
            }

            return BadRequest("Problem adding poster");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("set-main-poster/{movieId}/{posterId}")]
        public async Task<ActionResult> SetMainPoster(Guid movieId, Guid posterId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound();

            var poster = movie.Posters.FirstOrDefault(p => p.Id == posterId);

            if (poster == null) return NotFound();

            if (poster.IsMain) return BadRequest("This is already main poster");

            var currentMain = movie.Posters.FirstOrDefault(p => p.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            poster.IsMain = true;

            if (await _movieRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main poster");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-movie-poster/{movieId}/{posterId}")]
        public async Task<ActionResult> DeletePhoto(Guid movieId, Guid posterId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound();

            var poster = movie.Posters.FirstOrDefault(p => p.Id == posterId);

            if (poster == null) return NotFound();

            if (poster.IsMain) return BadRequest("You cannot delete main poster");

            if (poster.PublicId != null)
            {
                var results = await _photoService.DeletePhotoAsync(poster.PublicId);
                if (results.Error != null) return BadRequest(results.Error.Message);
            }

            movie.Posters.Remove(poster);

            if (await _movieRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete poster");
        }

        [Authorize]
        [HttpGet("user-rated-movies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetRatedMoviesForUser()
        {
            var userId = User.GetUserId();

            var ratedMovies = await _ratingRepository.GetRatedMoviesForUserAsync(userId);

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(ratedMovies));
        }
    }
}
