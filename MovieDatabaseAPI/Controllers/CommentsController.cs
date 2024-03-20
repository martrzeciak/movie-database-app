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
        private readonly ICommentRepository _commentRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IMovieRepository movieRepository,
            IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("movie-comments/{movieId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsForMovie(Guid movieId)
        {
            var comments = await _commentRepository.GetCommentsForMovieAsync(movieId);

            return Ok(_mapper.Map<IEnumerable<CommentDto>>(comments));
        }

        [Authorize]
        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> GetComment(Guid commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound();

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [Authorize]
        [HttpPost("{movieId}")]
        public async Task<ActionResult> AddComment(Guid movieId, CommentContentDto addCommentDto)
        {
            var userId = User.GetUserId();

            var movie = await _movieRepository.GetMovieByIdAsync(movieId);

            if (movie == null) return NotFound("Movie does not exists");

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CommentContent = addCommentDto.CommentContent,
                UserId = userId,
                MovieId = movieId
            };

            _commentRepository.AddComment(comment);

            if (await _commentRepository.SaveAllAsync())
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                var commentDto = _mapper.Map<CommentDto>(comment);

                commentDto.User = user != null ? _mapper.Map<MemberDto>(user) : null;

                return CreatedAtAction(nameof(GetComment), new { commentId = comment.Id }, commentDto);
            }

            return BadRequest("Failed to add comment");
        }

        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<ActionResult> EditComment(Guid commentId, CommentContentDto editCommentDto)
        {
            var userId = User.GetUserId();

            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound("Comment not found");

            if (comment.UserId != userId)
                return Unauthorized("You cannot edit this comment");

            comment.CommentContent = editCommentDto.CommentContent;

            if (!comment.IsEdited)
                comment.IsEdited = true;

            _commentRepository.Update(comment);

            if (await _commentRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update comment");
        }

        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(Guid commentId)
        {
            var userId = User.GetUserId();

            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound("Comment not found");

            if (comment.UserId != userId)
                return Unauthorized("You cannot delete this comment");

            _commentRepository.Delete(comment);

            if (await _commentRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to delete comment");
        }
    }
}
