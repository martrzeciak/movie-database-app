using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    //[Authorize]
    public class RatingController : BaseApiController
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public RatingController(IRatingRepository ratingRepository, IMovieRepository movieRepository, IUserRepository userRepository)
        {
            _ratingRepository = ratingRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        [HttpPost("rate-movie/{movieId}")]
        public async Task<ActionResult> RateMovie(Guid movieId,[FromQuery] int rating)
        {
            var userId = User.GetUserId();

            var movie = await _movieRepository.GetMovieAsync(movieId);

            if (movie == null) return NotFound("Movie does not exists.");

            var movieRating = await _ratingRepository.GetMovieRatingAsync(movieId, userId);

            if (movieRating != null)
            {
                if (movieRating.Rating == rating) return Ok();

                movieRating.Rating = rating;
            }
            else
            {
                movieRating = new MovieRating
                {
                    MovieId = movieId,
                    UserId = userId,
                    Rating = rating
                };

                movie.MovieRatings.Add(movieRating);
            }

            if (await _ratingRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to rate movie");
        }

        [HttpGet("movie-user-rating/{movieId}")]
        public async Task<ActionResult<int?>> GetUserRating(Guid movieId)
        {
            var userId = User.GetUserId();

            var ratingValue = await _ratingRepository.GetUserRatingValueAsync(movieId, userId);

            return Ok(ratingValue);
        }
    }
}
