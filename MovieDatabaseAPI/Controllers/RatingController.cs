using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    [Authorize]
    public class RatingController : BaseApiController
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public RatingController(IRatingRepository ratingRepository, IMovieRepository movieRepository, 
            IActorRepository actorRepository)
        {
            _ratingRepository = ratingRepository;
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        [HttpPost("rate-movie/{movieId}")]
        public async Task<ActionResult> RateMovie(Guid movieId,[FromQuery] int rating)
        {
            var userId = User.GetUserId();

            var movie = await _movieRepository.GetMovieAsync(movieId);

            if (movie == null) return NotFound("Movie does not exists.");

            var movieRating = await _ratingRepository.GetMovieRatingAsync(userId, movieId);

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
        public async Task<ActionResult<int>> GetMovieUserRating(Guid movieId)
        {
            var userId = User.GetUserId();

            var ratingValue = await _ratingRepository.GetUserRatingForMovieAsync(movieId, userId);

            return Ok(ratingValue);
        }

        [HttpPost("rate-actor/{actorId}")]
        public async Task<ActionResult> RateActor(Guid actorId,[FromQuery] int rating)
        {
            var userId = User.GetUserId();

            var actor = await _actorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound("Actor does not exists.");

            var actorRating = await _ratingRepository.GetActorRatingAsync(userId, actorId);

            if (actorRating != null)
            {
                if (actorRating.Rating == rating) return Ok();

                actorRating.Rating = rating;
            }
            else
            {
                actorRating = new ActorRating
                {
                    ActorId = actorId,
                    UserId = userId,
                    Rating = rating
                };

                actor.ActorRatings.Add(actorRating);
            }

            if (await _ratingRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to rate actor");
        }

        [HttpGet("actor-user-rating/{actorId}")]
        public async Task<ActionResult<int>> GetActorUserRating(Guid actorId)
        {
            var userId = User.GetUserId();

            var ratingValue = await _ratingRepository.GetUserRatingForActorAsync(actorId, userId);

            return Ok(ratingValue);
        }
    }
}
