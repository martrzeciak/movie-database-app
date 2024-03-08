using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class ActorsController : BaseApiController
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorsController(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _actorRepository.GetActors();

            if (!actors.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<ActorDto>>(actors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(Guid id)
        {
            var actor = await _actorRepository.GetActor(id);

            if (actor == null) return NotFound();

            return Ok(_mapper.Map<ActorDto>(actor));
        }

        [HttpGet("movie-actors/{id}")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActorsForMovie(Guid id)
        {
            var actorsForMovie = await _actorRepository.GetActorsForMovieAsync(id);

            return Ok(_mapper.Map<IEnumerable<ActorDto>>(actorsForMovie));
        }
    }
}
