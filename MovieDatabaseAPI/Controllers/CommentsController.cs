using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("movie-comments/{movieId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsForMovie(Guid movieId)
        {
            var comments = await _unitOfWork.CommentRepository.GetCommentsForMovieAsync(movieId);

            var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                foreach (var commentDto in commentDtos)
                {
                    commentDto.IsCommentLikedByCurrentUser = 
                        await _unitOfWork.CommentRepository.CheckIfCommentIsLikedByUser(userId, commentDto.Id);
                }
            }
            
            return Ok(commentDtos);
        }

        [Authorize]
        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> GetComment(Guid commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound();

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [Authorize]
        [HttpPost("{movieId}")]
        public async Task<ActionResult> AddComment(Guid movieId, CommentContentDto addCommentDto)
        {
            var userId = User.GetUserId();

            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound("Movie does not exists");

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CommentContent = addCommentDto.CommentContent,
                UserId = userId,
                MovieId = movieId
            };

            _unitOfWork.CommentRepository.AddComment(comment);

            if (await _unitOfWork.Complete())
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

                var commentDto = _mapper.Map<CommentDto>(comment);

                commentDto.User = _mapper.Map<MemberDto>(user);

                return CreatedAtAction(nameof(GetComment), new { commentId = comment.Id }, commentDto);
            }

            return BadRequest("Failed to add comment");
        }

        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<ActionResult> EditComment(Guid commentId, CommentContentDto editCommentDto)
        {
            var userId = User.GetUserId();

            var comment = await _unitOfWork.CommentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound("Comment not found");

            if (comment.UserId != userId)
                return BadRequest("You cannot edit this comment");

            comment.CommentContent = editCommentDto.CommentContent;

            if (!comment.IsEdited)
                comment.IsEdited = true;

            _unitOfWork.CommentRepository.Update(comment);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update comment");
        }

        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(Guid commentId)
        {
            var userId = User.GetUserId();

            var comment = await _unitOfWork.CommentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound("Comment not found");

            if (comment.UserId != userId)
                return Unauthorized("You cannot delete this comment");

            _unitOfWork.CommentRepository.Delete(comment);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to delete comment");
        }

        [Authorize]
        [HttpPost("add-like/{commentId}")]
        public async Task<ActionResult> AddLike(Guid commentId)
        {
            var userId = User.GetUserId();

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

            if (user == null) return NotFound("User does not exist");

            var comment = await _unitOfWork.CommentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound("Comment does not exists.");

            if (comment.Likes.Any(u => u.Id == userId))
                return BadRequest("You already liked this comment");

            comment.Likes.Add(user);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to add like");
        }

        [Authorize]
        [HttpDelete("remove-like/{commentId}")]
        public async Task<ActionResult> RemoveLike(Guid commentId)
        {
            var userId = User.GetUserId();

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

            if (user == null) return NotFound("User does not exist");

            var comment = await _unitOfWork.CommentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound("Comment does not exists.");

            if (!(comment.Likes.Any(u => u.Id == userId)))
                return BadRequest("You didn't like this comment");

            comment.Likes.Remove(user);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to remove like");
        }
    }
}
