using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet("movie-comments/{id}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsForMovie(Guid id)
        {
            var comments = await _commentRepository.GetCommentsForMovieAsync(id);

            if (!comments.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<CommentDto>>(comments));
        }
    }
}
