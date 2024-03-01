﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class ActorsController : BaseApiController
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorsController( IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _actorRepository.GetActors();

            if (!actors.Any()) return NotFound();

            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDetailsDto>> GetActor(Guid id)
        {
            var actor = await _actorRepository.GetActor(id);

            if (actor == null) return NotFound();

            return Ok(_mapper.Map<ActorDetailsDto>(actor));
        }
    }
}
