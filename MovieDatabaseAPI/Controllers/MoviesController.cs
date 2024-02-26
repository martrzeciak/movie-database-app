using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;

namespace MovieDatabaseAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public MoviesController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            var movies = await _dataContext.Movies.ToListAsync();

            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        [HttpGet("{id}")]
        public async Task<MovieDetailDto> GetMovie(Guid id)
        {
            var movie = await _dataContext.Movies
                .Include(g => g.Genres)
                .FirstAsync(x => x.Id == id);

            return _mapper.Map<MovieDetailDto>(movie);
        }
    }
}
