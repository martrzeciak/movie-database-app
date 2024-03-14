using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IActorRepository
    {
        Task<PagedList<ActorDto>> GetActorsAsync(ActorParams actorParams);
        Task<Actor?> GetActorAsync(Guid id);
        Task<PagedList<ActorDto>> GetActorsForMovieAsync(Guid id, PaginationParams paginationParams);
    }
}
