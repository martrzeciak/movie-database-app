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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        
        public MoviesController(IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MovieDto>>> GetMovies([FromQuery] MovieParams movieParams)
        {        
            var movies = await _unitOfWork.MovieRepository.GetMoviesAsync(movieParams);
     
            Response.AddPaginationHeader(new PaginationHeader(
                movies.CurrentPage, movies.PageSize, movies.TotalCount, movies.TotalPages));

            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> GetMovie(Guid movieId)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movie);

            movieDto.AverageRating = await _unitOfWork.RatingRepository.GetAverageRatingForMovieAsync(movieId);
            movieDto.RatingCount = await _unitOfWork.RatingRepository.GetRatingCountForMovieAsync(movieId);
            movieDto.MoviePosition = await _unitOfWork.MovieRepository.GetMoviePositionAsync(movieId);

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                movieDto.IsOnWantToWatchMovie = 
                    await _unitOfWork.MovieRepository
                        .CheckIsMovieOnUserWantToWatchListAsync(movie.Id, userId);
            }
            
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
                    var genre = await _unitOfWork.GenreRepository.GetGenreByIdAsync(genreId);
                    if (genre != null)
                        movie.Genres.Add(genre);
                }
            }


            if (movieForCreationDto.ActorIds.Count != 0)
            {
                foreach (var actorId in movieForCreationDto.ActorIds)
                {
                    var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);
                    if (actor != null)
                        movie.Actors.Add(actor);
                }
            }

            _unitOfWork.MovieRepository.Add(movie);
            var movieDto = _mapper.Map<MovieDto>(movie);

            if (await _unitOfWork.Complete())
                return CreatedAtAction(nameof(GetMovie), new { movieId = movie.Id }, movieDto);

            return BadRequest("Failed to add movie");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDto>> UpdateMovie(Guid id, MovieForCreationDto movieForUpdateDto)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieForUpdateAsync(id);

            if (movie == null) return NotFound();

            _mapper.Map(movieForUpdateDto, movie);

            movie.Genres.Clear();
            if (movieForUpdateDto.GenreIds.Count != 0)
            {
                foreach (var genreId in movieForUpdateDto.GenreIds)
                {
                    var genre = await _unitOfWork.GenreRepository.GetGenreByIdAsync(genreId);
                    if (genre != null)
                        movie.Genres.Add(genre);
                }
            }

            movie.Actors.Clear();
            if (movieForUpdateDto.ActorIds.Count != 0)
            {
                foreach (var actorId in movieForUpdateDto.ActorIds)
                {
                    var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);
                    if (actor != null)
                        movie.Actors.Add(actor);
                }
            }

            _unitOfWork.MovieRepository.Update(movie);

            if (await _unitOfWork.Complete())
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
            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(id);

            if (movie == null) return NotFound();

            _unitOfWork.MovieRepository.Delete(movie);

            if (await _unitOfWork.Complete())
                return NoContent();
            
            return BadRequest("Failed to delete movie");
        }

        [HttpGet("actor-movies/{id}")]
        public async Task<ActionResult<PagedList<MovieDto>>> GetMoviesForActor(Guid id,
            [FromQuery] PaginationParams paginationParams)
        {
            var moviesForActor = await _unitOfWork.MovieRepository.GetMoviesForActorAsync(id, paginationParams);

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
            var suggestions = await _unitOfWork.MovieRepository.GetSearchSuggestionsAsync(query);

            return Ok(_mapper.Map<IEnumerable<MovieNameDto>>(suggestions));
        }

        [HttpGet("movie-name")]
        public async Task<ActionResult<IEnumerable<MovieNameDto>>> GetMovieNameList()
        {
            var movies = await _unitOfWork.MovieRepository.GetMovieNameListAsync();

            return Ok(_mapper.Map<IEnumerable<MovieNameDto>>(movies));
        }

        [HttpGet("movie-name/{actorId}")]
        public async Task<ActionResult<IEnumerable<MovieNameDto>>> GetMovieNameListForActor(Guid actorId)
        {
            var movies = await _unitOfWork.MovieRepository.GetMovieNameListForActorAsync(actorId);

            return Ok(_mapper.Map<IEnumerable<MovieNameDto>>(movies));
        }

        [Authorize]
        [HttpPost("add-movie-poster/{movieId}")]
        public async Task<ActionResult<UserImageDto>> AddMoviePoster(Guid movieId, IFormFile file)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound();

            var results = await _imageService.AddImageAsync(file, "movie");

            if (results.Error != null) return BadRequest(results.Error.Message);

            var poster = new Poster
            {
                PosterUrl = results.SecureUrl.AbsoluteUri,
                PublicId = results.PublicId
            };

            if (movie.Posters.Count == 0) poster.IsMain = true;

            movie.Posters.Add(poster);

            if (await _unitOfWork.Complete())
            {
                return CreatedAtAction(nameof(GetMovie),
                    new { movieId = movie.Id },
                    _mapper.Map<PosterDto>(poster));
            }

            return BadRequest("Problem adding poster");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("set-main-poster/{movieId}/{posterId}")]
        public async Task<ActionResult> SetMainPoster(Guid movieId, Guid posterId)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound();

            var poster = movie.Posters.FirstOrDefault(p => p.Id == posterId);

            if (poster == null) return NotFound();

            if (poster.IsMain) return BadRequest("This poster is already main poster");

            var currentMain = movie.Posters.FirstOrDefault(p => p.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            poster.IsMain = true;

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to set main poster");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-movie-poster/{movieId}/{posterId}")]
        public async Task<ActionResult> DeletePoster(Guid movieId, Guid posterId)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound();

            var poster = movie.Posters.FirstOrDefault(p => p.Id == posterId);

            if (poster == null) return NotFound();

            if (poster.IsMain) return BadRequest("You cannot delete main poster");

            if (poster.PublicId != null)
            {
                var results = await _imageService.DeleteImageAsync(poster.PublicId);
                if (results.Error != null) return BadRequest(results.Error.Message);
            }

            movie.Posters.Remove(poster);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete poster");
        }

        [Authorize]
        [HttpGet("user-rated-movies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetRatedMoviesForUser()
        {
            var userId = User.GetUserId();

            var ratedMovies = await _unitOfWork.RatingRepository.GetRatedMoviesForUserAsync(userId);

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(ratedMovies));
        }

        [HttpGet("random-movie")]
        public async Task<ActionResult<Guid>> GetRandomMovie()
        {
            var movieId = await _unitOfWork.MovieRepository.GetRandomMovieIdAsync();

            if (movieId == null) return NotFound();

            return Ok(movieId);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> SearchMovies([FromQuery] string query)
        {
            var searchResults = await _unitOfWork.MovieRepository.SearchMoviesAsync(query);

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(searchResults));
        }

        [HttpGet("suggested-movies/{movieId}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetRandomSuggestionsByGenres(Guid movieId)
        {
            var suggestedMovies = await _unitOfWork.MovieRepository.GetRandomSuggestionsByGenresAsync(movieId, 3);

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(suggestedMovies));
        }

        [Authorize]
        [HttpPost("add-want-to-watch-movie/{movieId}")]
        public async Task<ActionResult> AddToFromWantToWatch(Guid movieId)
        {
            var userId = User.GetUserId();

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound("Movie does not exists.");

            if (user != null && user.Movies.Any(m => m.Id == movieId))
                return BadRequest($"{ movie.Title } is already on the want to watch list.");
            
            if (user != null)
                movie.Users.Add(user);
            
            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to add movie to want to watch list");
        }

        [Authorize]
        [HttpDelete("remove-want-to-watch-movie/{movieId}")]
        public async Task<ActionResult> RemoveFromWantToWatch(Guid movieId)
        {
            var userId = User.GetUserId();

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound("Movie does not exist.");

            if (user == null || !user.Movies.Any(m => m.Id == movieId))
                return BadRequest($"{movie.Title} is not on the want-to-watch list.");

            movie.Users.Remove(user);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to remove movie from want to watch list");
        }

        [Authorize]
        [HttpGet("user-want-to-watch-list")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetUserWantToWatchList()
        {
            var userId = User.GetUserId();
            
            var movies = await _unitOfWork.MovieRepository.GetUserWantToWatchMovieListAsync(userId);

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(movies));
        }
    }
}
