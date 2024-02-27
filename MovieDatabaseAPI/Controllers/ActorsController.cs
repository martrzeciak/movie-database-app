using AutoMapper;
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
        public async Task<IEnumerable<ActorDto>> GetActors()
        {
            var actors = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ActorDto>>(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActorDetailsDto> GetActor(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .Include(m => m.Movies)
                    .ThenInclude(p => p.Poster)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ActorDetailsDto>(actor);
        }
    }
}
