using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    [Authorize]
    public class RatingController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("rate-movie/{movieId}")]
        public async Task<ActionResult> RateMovie(Guid movieId,[FromQuery] int rating)
        {
            var userId = User.GetUserId();

            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound("Movie does not exists.");

            var movieRating = await _unitOfWork.RatingRepository.GetMovieRatingAsync(userId, movieId);

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

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to rate movie");
        }

        [HttpDelete("remove-movie-rating/{movieId}")]
        public async Task<ActionResult> RemoveMovieRating(Guid movieId)
        {
            var userId = User.GetUserId();

            var movieRating = await _unitOfWork.RatingRepository.GetMovieRatingAsync(userId, movieId);

            if (movieRating == null)
            {
                return NotFound("Movie rating not found.");
            }

            _unitOfWork.RatingRepository.RemoveMovieRating(movieRating);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to remove movie rating.");
        }

        [HttpGet("movie-user-rating/{movieId}")]
        public async Task<ActionResult<int>> GetMovieUserRating(Guid movieId)
        {
            var userId = User.GetUserId();

            var ratingValue = await _unitOfWork.RatingRepository.GetUserRatingForMovieAsync(movieId, userId);

            return Ok(ratingValue);
        }

        [HttpGet("rated-movies")]
        public async Task<ActionResult<PagedList<MovieDto>>> GetRatedMoviesForUser()
        {
            var userId = User.GetUserId();

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

            if (user == null) NotFound();

            return Ok();
        }

        [HttpPost("rate-actor/{actorId}")]
        public async Task<ActionResult> RateActor(Guid actorId,[FromQuery] int rating)
        {
            var userId = User.GetUserId();

            var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound("Actor does not exists.");

            var actorRating = await _unitOfWork.RatingRepository.GetActorRatingAsync(userId, actorId);

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

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to rate actor");
        }

        [HttpDelete("remove-actor-rating/{actorId}")]
        public async Task<ActionResult> RemoveActorRating(Guid actorId)
        {
            var userId = User.GetUserId();

            var actorRating = await _unitOfWork.RatingRepository.GetActorRatingAsync(userId, actorId);

            if (actorRating == null)
            {
                return NotFound("Actor rating not found.");
            }

            _unitOfWork.RatingRepository.RemoveActorRating(actorRating);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to remove actor rating.");
        }

        [HttpGet("actor-user-rating/{actorId}")]
        public async Task<ActionResult<int>> GetUserRatingForActor(Guid actorId)
        {
            var userId = User.GetUserId();

            var ratingValue = await _unitOfWork.RatingRepository.GetUserRatingForActorAsync(actorId, userId);

            return Ok(ratingValue);
        }
    }
}
