using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class GenresController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenresController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            var genres = await _unitOfWork.GenreRepository.GetGenresAsync();

            return Ok(_mapper.Map<IEnumerable<GenreDto>>(genres));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenre(Guid id)
        {
            var genre = await _unitOfWork.GenreRepository.GetGenreByIdAsync(id);

            if (genre == null) return NotFound();

            return Ok(_mapper.Map<GenreDto>(genre));
        }
    }
}
