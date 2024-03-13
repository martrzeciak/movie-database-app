﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Extensions;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MovieDto>>> GetMovies([FromQuery] MovieParams movieParams)
        {
            var movies = await _movieRepository.GetMoviesAsync(movieParams);

            Response.AddPaginationHeader(new PaginationHeader(
                movies.CurrentPage, movies.PageSize, movies.TotalCount, movies.TotalPages));

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);

            if (movie == null) return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movie);

            movieDto.AverageRating = await _movieRepository.GetAverageRatingForMovieAsync(id);
            movieDto.RatingCount = await _movieRepository.GetRatingCountForMovieAsync(id);

            return Ok(movieDto);
        }

        [HttpGet("actor-movies/{id}")]
        public async Task<ActionResult<PagedList<MovieDto>>> GetMoviesForActor(Guid id,
            [FromQuery] PaginationParams paginationParams)
        {
            var moviesForActor = await _movieRepository.GetMoviesForActorAsync(id, paginationParams);

            Response.AddPaginationHeader(new PaginationHeader(
                moviesForActor.CurrentPage,
                moviesForActor.PageSize,
                moviesForActor.TotalCount,
                moviesForActor.TotalPages));

            return Ok(moviesForActor);
        }

        [HttpGet("search-suggestions")]
        public async Task<ActionResult<IEnumerable<string>>> GetSearchSuggestions([FromQuery] string query)
        {
            var suggestions = await _movieRepository.GetSearchSuggestionsAsync(query);

            return Ok(suggestions);
        }
    }
}
