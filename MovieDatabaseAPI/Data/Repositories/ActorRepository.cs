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

            if (!string.IsNullOrEmpty(actorParams.Gender))
            {
                query = query.Where(a => a.Gender == actorParams.Gender);
            }

            query = actorParams.OrderBy switch
            {
                "alphabetical" => query.OrderBy(m => m.LastName),
                _ => query.OrderByDescending(u => u.ActorRatings.Count())
            };

            return await PagedList<ActorDto>.CreateAsync(
                query.ProjectTo<ActorDto>(_mapper.ConfigurationProvider),
                actorParams.PageNumber, actorParams.PageSize);
        }

        public async Task<Actor?> GetActorAsync(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<Actor?> GetActorForUpdateAsync(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<PagedList<ActorDto>> GetActorsForMovieAsync(Guid id, PaginationParams paginationParams)
        {
            var query = _dataContext.Actors.AsQueryable();

            query = query.Where(actor => actor.Movies.Any(movie => movie.Id == id));

            return await PagedList<ActorDto>.CreateAsync(
                query.ProjectTo<ActorDto>(_mapper.ConfigurationProvider),
                paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<IEnumerable<Actor>> GetActorNameListAsync()
        {
            var actors = await _dataContext.Actors.ToListAsync();

            return actors;
        }

        public async Task<IEnumerable<Actor>> GetActorNameListForMovieAsync(Guid movieId)
        {
            var actors = await _dataContext.Actors
                .Where(actor => actor.Movies.Any(movie => movie.Id == movieId))
                .ToListAsync();

            return actors;
        }

        public void Add(Actor actor)
        {
            _dataContext.Actors.Add(actor);
        }

        public void Update(Actor actor)
        {
            _dataContext.Entry(actor).State = EntityState.Modified;
        }

        public void Delete(Actor actor)
        {
            _dataContext.Actors.Remove(actor);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
