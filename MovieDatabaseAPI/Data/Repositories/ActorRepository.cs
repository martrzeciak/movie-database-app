using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ActorRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<PagedList<ActorDto>> GetActorsAsync(ActorParams actorParams)
        {
            var query = _dataContext.Actors.AsQueryable();

            return await PagedList<ActorDto>.CreateAsync(
                query.ProjectTo<ActorDto>(_mapper.ConfigurationProvider),
                actorParams.PageNumber, actorParams.PageSize);
        }

        public async Task<Actor?> GetActor(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<PagedList<ActorDto>> GetActorsForMovieAsync(Guid id, PaginationParams paginationParams)
        {
            var query = _dataContext.Actors.AsQueryable();

            query.Where(actor => actor.Movies.Any(movie => movie.Id == id));

            return await PagedList<ActorDto>.CreateAsync(
                query.ProjectTo<ActorDto>(_mapper.ConfigurationProvider),
                paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
