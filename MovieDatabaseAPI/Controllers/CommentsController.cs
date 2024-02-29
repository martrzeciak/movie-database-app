using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;

namespace MovieDatabaseAPI.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CommentsController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet("movie-comments/{id}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsForMovie(Guid id)
        {
            var comments = _dataContext.Comments
                .Include(u => u.User)
                .Where(comment => comment.Movie.Id == id);

            if (comments == null) return NotFound();

            return Ok(_mapper.Map<IEnumerable<CommentDto>>(comments));
        }
    }
}
