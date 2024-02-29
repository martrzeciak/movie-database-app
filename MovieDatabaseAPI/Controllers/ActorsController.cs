using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;

namespace MovieDatabaseAPI.Controllers
{
    public class ActorsController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ActorsController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = _dataContext.Actors
                .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            if (actors == null) return NotFound();

            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDetailsDto>> GetActor(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null) return NotFound();

            return Ok(_mapper.Map<ActorDetailsDto>(actor));
        }
    }
}
