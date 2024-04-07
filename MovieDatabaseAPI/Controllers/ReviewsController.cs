using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class ReviewsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(Guid reviewId)
        {
            var review = await _unitOfWork.ReviewRepository.GetReviewByIdAsync(reviewId);

            if (review == null) return NotFound("Review does not exist");

            return Ok(_mapper.Map<ReviewDto>(review));
        }

        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovie(Guid movieId)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetReviewsForMovieAsync(movieId);

            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));
        }

        [HttpGet("user/{username}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForUser(string username)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetReviewsForUserAsync(username);

            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));
        }

        [Authorize(Roles = "Reviewer")]
        [HttpPost("{movieId}")]
        public async Task<ActionResult> AddReview(Guid movieId, ReviewForCreationDto reviewForCreationDto)
        {
            var userId = User.GetUserId();

            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound("Movie does not exists");

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Rating = reviewForCreationDto.Rating,
                ReviewContent = reviewForCreationDto.ReviewContent,
                UserId = userId,
                MovieId = movieId
            };

            _unitOfWork.ReviewRepository.AddReview(review);

            if (await _unitOfWork.Complete())
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

                var reviewDto = _mapper.Map<ReviewDto>(review);

                reviewDto.User = _mapper.Map<MemberDto>(user);

                return CreatedAtAction(nameof(GetReview), new { reviewId = review.Id }, reviewDto);
            }

            return BadRequest("Failed to add review");
        }

        [Authorize(Roles = "Reviewer")]
        [HttpPut("{reviewId}")]
        public async Task<ActionResult> EditReview(Guid reviewId, ReviewForCreationDto editReviewDto)
        {
            var userId = User.GetUserId();

            var review = await _unitOfWork.ReviewRepository.GetReviewByIdAsync(reviewId);

            if (review == null) return NotFound("Review not found");

            if (review.UserId != userId)
                return BadRequest("You cannot edit this comment");

            review.Rating = editReviewDto.Rating;
            review.ReviewContent = editReviewDto.ReviewContent;

            _unitOfWork.ReviewRepository.UpdateReview(review);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update review");
        }

        [Authorize(Roles = "Admin, Reviewer")]
        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> DeleteReview(Guid reviewId)
        {
            var userId = User.GetUserId();

            var review = await _unitOfWork.ReviewRepository.GetReviewByIdAsync(reviewId);

            if (review == null) return NotFound("Review not found");

            if (review.UserId != userId)
                return Unauthorized("You cannot delete this review");

            _unitOfWork.ReviewRepository.RemoveReview(review);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to delete review");
        }
    }
}
