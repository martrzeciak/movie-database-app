using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly DataContext _dataContext;

        public ActorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            var actors = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .ToListAsync();

            return actors;
        }

        public async Task<Actor?> GetActor(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<IEnumerable<Actor>> GetActorsForMovieAsync(Guid id)
        {
            var moviesForActor = await _dataContext.Actors
                .Where(actor => actor.Movies.Any(movie => movie.Id == id))
                .ToListAsync();

            return moviesForActor;
        }
    }
}
