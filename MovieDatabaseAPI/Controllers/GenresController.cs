using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;

namespace MovieDatabaseAPI.Controllers
{
    public class GenresController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GenresController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GenreDto>> GetGenres()
        {
            var genres = await _dataContext.Genres.ToListAsync();

            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        [HttpGet("{id}")]
        public async Task<GenreDto> GetGenre(Guid id)
        {
            var genre = await _dataContext.Genres
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<GenreDto>(genre);
        }
    }
}
