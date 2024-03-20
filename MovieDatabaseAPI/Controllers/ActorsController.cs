using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Extensions;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class ActorsController : BaseApiController
    {
        private readonly IActorRepository _actorRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public ActorsController(IActorRepository actorRepository, IRatingRepository ratingRepository,
            IMovieRepository movieRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _ratingRepository = ratingRepository;
            _movieRepository = movieRepository;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddActor(ActorForCreationDto actorForCreationDto)
        {
            var actor = _mapper.Map<Actor>(actorForCreationDto);

            if (actorForCreationDto.MovieIds.Count != 0)
            {
                foreach (var movieId in actorForCreationDto.MovieIds)
                {
                    var movie = await _movieRepository.GetMovieByIdAsync(movieId);
                    if (movie != null)
                        actor.Movies.Add(movie);
                }
            }

            _actorRepository.Add(actor);
            var actorDto = _mapper.Map<ActorDto>(actor);

            if (await _actorRepository.SaveAllAsync())
                return CreatedAtAction(nameof(GetActor), new { actorId = actor.Id }, actorDto);

            return BadRequest("Failed to add actor");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActor(Guid id, ActorForCreationDto actorForUpdateDto)
        {
            var actor = await _actorRepository.GetActorForUpdateAsync(id);

            if (actor == null) return NotFound();

            _mapper.Map(actorForUpdateDto, actor);

            actor.Movies.Clear();
            if (actorForUpdateDto.MovieIds.Count != 0)
            {
                foreach (var movieId in actorForUpdateDto.MovieIds)
                {
                    var movie = await _movieRepository.GetMovieByIdAsync(movieId);
                    if (movie != null)
                        actor.Movies.Add(movie);
                }
            }

            _actorRepository.Update(actor);

            if (await _movieRepository.SaveAllAsync())
            {
                var updatedActorDto = _mapper.Map<ActorDto>(actor);
                return Ok(updatedActorDto);
            }

            return BadRequest("Failed to update actor");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(Guid id)
        {
            var actor = await _actorRepository.GetActorAsync(id);

            if (actor == null) return NotFound();

            _actorRepository.Delete(actor);

            if (await _actorRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Failed to delete actor");
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

        [HttpGet("actor-name")]
        public async Task<ActionResult<IEnumerable<ActorNameDto>>> GetActorNameList()
        {
            var actors = await _actorRepository.GetActorNameListAsync();

            return Ok(_mapper.Map<IEnumerable<ActorNameDto>>(actors));
        }

        [HttpGet("actor-name/{movieId}")]
        public async Task<ActionResult<IEnumerable<ActorNameDto>>> GetActorNameListForMovie(Guid movieId)
        {
            var actors = await _actorRepository.GetActorNameListForMovieAsync(movieId);

            return Ok(_mapper.Map<IEnumerable<ActorNameDto>>(actors));
        }
    }
}
