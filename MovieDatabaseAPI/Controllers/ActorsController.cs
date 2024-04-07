using API.Extensions;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ActorsController(IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ActorDto>>> GetActors([FromQuery] ActorParams actorParams)
        {
            var actors = await _unitOfWork.ActorRepository.GetActorsAsync(actorParams);

            Response.AddPaginationHeader(new PaginationHeader(
                actors.CurrentPage, actors.PageSize, actors.TotalCount, actors.TotalPages));

            return Ok(actors);
        }

        [HttpGet("{actorId}")]
        public async Task<ActionResult<ActorDto>> GetActor(Guid actorId)
        {
            var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound();

            var actorDto = _mapper.Map<ActorDto>(actor);

            actorDto.AverageRating = await _unitOfWork.RatingRepository.GetAverageRatingForActorAsync(actorId);
            actorDto.RatingCount = await _unitOfWork.RatingRepository.GetRatingCountForActorAsync(actorId);
            actorDto.ActorPosition = await _unitOfWork.ActorRepository.GetActorPositionAsync(actorId);

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
                    var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);
                    if (movie != null)
                        actor.Movies.Add(movie);
                }
            }

            _unitOfWork.ActorRepository.Add(actor);
            var actorDto = _mapper.Map<ActorDto>(actor);

            if (await _unitOfWork.Complete())
                return CreatedAtAction(nameof(GetActor), new { actorId = actor.Id }, actorDto);

            return BadRequest("Failed to add actor");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActor(Guid id, ActorForCreationDto actorForUpdateDto)
        {
            var actor = await _unitOfWork.ActorRepository.GetActorForUpdateAsync(id);

            if (actor == null) return NotFound();

            _mapper.Map(actorForUpdateDto, actor);

            actor.Movies.Clear();
            if (actorForUpdateDto.MovieIds.Count != 0)
            {
                foreach (var movieId in actorForUpdateDto.MovieIds)
                {
                    var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);
                    if (movie != null)
                        actor.Movies.Add(movie);
                }
            }

            _unitOfWork.ActorRepository.Update(actor);

            if (await _unitOfWork.Complete())
            {
                var updatedActorDto = _mapper.Map<ActorDto>(actor);
                return Ok(updatedActorDto);
            }

            return BadRequest("Failed to update actor");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{actorId}")]
        public async Task<ActionResult> DeleteActor(Guid actorId)
        {
            var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound();

            _unitOfWork.ActorRepository.Delete(actor);

            if (await _unitOfWork.Complete())
                return NoContent();
            
            return BadRequest("Failed to delete actor");
        }

        [HttpGet("movie-actors/{movieId}")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActorsForMovie(Guid movieId)
        {
            var actorsForMovie = await _unitOfWork.ActorRepository.GetActorsForMovieAsync(movieId);

            return Ok(_mapper.Map<IEnumerable<ActorDto>>(actorsForMovie));
        }

        [HttpGet("actor-name")]
        public async Task<ActionResult<IEnumerable<ActorNameDto>>> GetActorNameList()
        {
            var actors = await _unitOfWork.ActorRepository.GetActorNameListAsync();

            return Ok(_mapper.Map<IEnumerable<ActorNameDto>>(actors));
        }

        [HttpGet("actor-name/{movieId}")]
        public async Task<ActionResult<IEnumerable<ActorNameDto>>> GetActorNameListForMovie(Guid movieId)
        {
            var actors = await _unitOfWork.ActorRepository.GetActorNameListForMovieAsync(movieId);

            return Ok(_mapper.Map<IEnumerable<ActorNameDto>>(actors));
        }

        [Authorize]
        [HttpPost("add-actor-image/{actorId}")]
        public async Task<ActionResult<UserImageDto>> AddActorImage(Guid actorId, IFormFile file)
        {
            var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound();

            var results = await _imageService.AddImageAsync(file, "actor");

            if (results.Error != null) return BadRequest(results.Error.Message);

            var image = new ActorImage
            {
                ImageUrl = results.SecureUrl.AbsoluteUri,
                PublicId = results.PublicId
            };

            if (actor.Images.Count == 0) image.IsMain = true;

            actor.Images.Add(image);

            if (await _unitOfWork.Complete())
            {
                return CreatedAtAction(nameof(GetActor),
                    new { actorId = actor.Id },
                    _mapper.Map<ActorImageDto>(image));
            }

            return BadRequest("Problem adding image");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("set-main-image/{actorId}/{imageId}")]
        public async Task<ActionResult> SetMainImage(Guid actorId, Guid imageId)
        {
            var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound();

            var image = actor.Images.FirstOrDefault(p => p.Id == imageId);

            if (image == null) return NotFound();

            if (image.IsMain) return BadRequest("This image is already main image");

            var currentMain = actor.Images.FirstOrDefault(p => p.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            image.IsMain = true;

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to set main image");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-actor-image/{actorId}/{imageId}")]
        public async Task<ActionResult> DeleteImage(Guid actorId, Guid imageId)
        {
            var actor = await _unitOfWork.ActorRepository.GetActorAsync(actorId);

            if (actor == null) return NotFound();

            var image = actor.Images.FirstOrDefault(p => p.Id == imageId);

            if (image == null) return NotFound();

            if (image.IsMain) return BadRequest("You cannot delete main image");

            if (image.PublicId != null)
            {
                var results = await _imageService.DeleteImageAsync(image.PublicId);
                if (results.Error != null) return BadRequest(results.Error.Message);
            }

            actor.Images.Remove(image);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete image");
        }

        [Authorize]
        [HttpGet("user-rated-actors")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetRatedActorsForUser()
        {
            var userId = User.GetUserId();

            var ratedActors = await _unitOfWork.RatingRepository.GetRatedActorsForUserAsync(userId);

            return Ok(_mapper.Map<IEnumerable<ActorDto>>(ratedActors));
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> SearchActors([FromQuery] string query)
        {
            var searchResults = await _unitOfWork.ActorRepository.SearchActorsAsync(query);

            return Ok(_mapper.Map<IEnumerable<ActorDto>>(searchResults));
        }

        [HttpGet("search-suggestions")]
        public async Task<ActionResult<IEnumerable<ActorNameDto>>> GetSearchSuggestions([FromQuery] string query)
        {
            var suggestions = await _unitOfWork.ActorRepository.GetSearchSuggestionsAsync(query);

            return Ok(_mapper.Map<IEnumerable<ActorNameDto>>(suggestions));
        }
    }
}
