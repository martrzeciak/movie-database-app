using AutoMapper;
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

            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(_mapper.ConfigurationProvider), 
                movieParams.PageNumber, movieParams.PageSize);
        }

        public async Task<Movie?> GetMovieAsync(Guid id)
        {
            var movie = await _dataContext.Movies
                .Include(g => g.Genres)
                .Include(p => p.Poster)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<PagedList<MovieDto>> GetMoviesForActorAsync(Guid id, PaginationParams paginationParams)
        {
            var query = _dataContext.Movies.AsQueryable();
            
            query.Where(movie => movie.Actors.Any(actor => actor.Id == id));

            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(_mapper.ConfigurationProvider), 
                paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
