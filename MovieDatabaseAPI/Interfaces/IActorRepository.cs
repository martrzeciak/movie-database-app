using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IActorRepository
    {
        Task<PagedList<ActorDto>> GetActorsAsync(ActorParams actorParams);
        Task<Actor?> GetActorAsync(Guid id);
        Task<Actor?> GetActorForUpdateAsync(Guid id);
        Task<PagedList<ActorDto>> GetActorsForMovieAsync(Guid id, PaginationParams paginationParams);
        Task<IEnumerable<Actor>> GetActorNameListAsync();
        Task<IEnumerable<Actor>> GetActorNameListForMovieAsync(Guid movieId);
        void Add(Actor actor);
        void Update(Actor actor);
        void Delete(Actor actor);
        Task<bool> SaveAllAsync();
    }
}
