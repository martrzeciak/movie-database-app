using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Extensions;
using MovieDatabaseAPI.Helpers;
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
        public async Task<ActionResult<PagedList<ActorDto>>> GetActors([FromQuery] ActorParams actorParams)
        {
            var actors = await _actorRepository.GetActorsAsync(actorParams);

            Response.AddPaginationHeader(new PaginationHeader(
                actors.CurrentPage, actors.PageSize, actors.TotalCount, actors.TotalPages));

            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(Guid id)
        {
            var actor = await _actorRepository.GetActor(id);

            if (actor == null) return NotFound();

            return Ok(_mapper.Map<ActorDto>(actor));
        }

        [HttpGet("movie-actors/{id}")]
        public async Task<ActionResult<PagedList<ActorDto>>> GetActorsForMovie(Guid id,
            [FromQuery] PaginationParams paginationParams)
        {
            var actorsForMovie = await _actorRepository.GetActorsForMovieAsync(id, paginationParams);

            Response.AddPaginationHeader(new PaginationHeader(
                actorsForMovie.CurrentPage,
                actorsForMovie.PageSize,
                actorsForMovie.TotalCount,
                actorsForMovie.TotalPages));

            return Ok(actorsForMovie);
        }
    }
}
