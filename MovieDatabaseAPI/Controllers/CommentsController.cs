using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
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
    }
}
