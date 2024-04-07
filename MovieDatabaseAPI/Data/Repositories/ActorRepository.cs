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
                .Include(i => i.Images)
                .FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<Actor?> GetActorForUpdateAsync(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.Images)
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<IEnumerable<Actor>> GetActorsForMovieAsync(Guid id)
        {
            var actors = await _dataContext.Actors
                .Include(r => r.ActorRatings)
                .Include(i => i.Images)
                .Where(actor => actor.Movies.Any(movie => movie.Id == id))
                .ToListAsync();


            return actors;
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

        public async Task<IEnumerable<Actor>> GetSearchSuggestionsAsync(string query)
        {
            return await _dataContext.Actors
                .Where(m => m.LastName.Contains(query) || m.FirstName.Contains(query))
                .Take(5)
                .ToListAsync();
        }

        public async Task<int> GetActorPositionAsync(Guid actorId)
        {
            var query = _dataContext.Actors.AsQueryable();

            query = query.OrderByDescending(m => m.ActorRatings.Count());

            var actorIds = await query.Select(m => m.Id).ToListAsync();

            var actorPosition = actorIds.IndexOf(actorId) + 1;

            return actorPosition;
        }

        public async Task<IEnumerable<Actor>> SearchActorsAsync(string query)
        {
            var searchResults = await _dataContext.Actors
                .Include(p => p.Images)
                .Include(mr => mr.ActorRatings)
                .Where(movie => movie.LastName.Contains(query) || movie.FirstName.Contains(query))
                .ToListAsync();

            return searchResults;
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
    }
}
