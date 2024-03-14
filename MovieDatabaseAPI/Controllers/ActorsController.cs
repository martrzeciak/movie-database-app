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
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public ActorsController(IActorRepository actorRepository, IRatingRepository ratingRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _ratingRepository = ratingRepository;
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

        [HttpGet("{actorId}")]
        public async Task<ActionResult<ActorDto>> GetActor(Guid actorId)
        {
            var actor = await _actorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound();

            var actorDto = _mapper.Map<ActorDto>(actor);

            actorDto.AverageRating = await _ratingRepository.GetAverageRatingForActorAsync(actorId);
            actorDto.RatingCount = await _ratingRepository.GetRatingCountForActorAsync(actorId);

            return Ok(actorDto);
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
