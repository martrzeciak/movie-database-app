﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public MovieRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<PagedList<MovieDto>> GetMoviesAsync(MovieParams movieParams)
        {
            var query = _dataContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(movieParams.ReleaseDate))
            {
                query = query.Where(m => m.ReleaseDate == int.Parse(movieParams.ReleaseDate));
            }

            if (!string.IsNullOrEmpty(movieParams.Genre))
            {
                query = query.Where(m => m.Genres.Any(g => g.Name == movieParams.Genre));
            }

            query = movieParams.OrderBy switch
            {
                "alphabetical" => query.OrderByDescending(m => m.Title),
                _ => query.OrderByDescending(u => u.MovieRatings.Count())
            };

            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(_mapper.ConfigurationProvider),
                movieParams.PageNumber, movieParams.PageSize);
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            var movie = await _dataContext.Movies
                .Include(g => g.Genres)
                .Include(p => p.Posters)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<PagedList<MovieDto>> GetMoviesForActorAsync(Guid id, PaginationParams paginationParams)
        {
            var query = _dataContext.Movies.AsQueryable();

            query = query.Where(movie => movie.Actors.Any(actor => actor.Id == id));

            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(_mapper.ConfigurationProvider),
                paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<IEnumerable<Movie>> GetSearchSuggestionsAsync(string query)
        {
            return await _dataContext.Movies
                .Where(m => m.Title.Contains(query))
                .Distinct()
                .Take(5)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieForUpdateAsync(Guid id)
        {
            var movie = await _dataContext.Movies
                .Include(g => g.Genres)
                .Include(p => p.Posters)
                .Include(a => a.Actors)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMovieNameListAsync()
        {
            var movies = await _dataContext.Movies.ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMovieNameListForActorAsync(Guid actorId)
        {
            var movies = await _dataContext.Movies
                .Where(movie => movie.Actors.Any(actor => actor.Id == actorId))
                .ToListAsync();

            return movies;
        }

        public async Task<int> GetMoviePositionAsync(Guid movieId)
        {
            var query = _dataContext.Movies.AsQueryable();

            query = query.OrderByDescending(m => m.MovieRatings.Count());

            var movieIds = await query.Select(m => m.Id).ToListAsync();

            var moviePosition = movieIds.IndexOf(movieId) + 1;

            return moviePosition;
        }

        public void Add(Movie movie)
        {
            _dataContext.Movies.Add(movie);
        }

        public void Update(Movie movie)
        {
            _dataContext.Entry(movie).State = EntityState.Modified;
        }

        public void Delete(Movie movie)
        {
            _dataContext.Movies.Remove(movie);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
