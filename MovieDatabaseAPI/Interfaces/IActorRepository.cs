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
        Task<IEnumerable<Actor>> GetActorsForMovieAsync(Guid id);
        Task<IEnumerable<Actor>> GetActorNameListAsync();
        Task<IEnumerable<Actor>> GetActorNameListForMovieAsync(Guid movieId);
        Task<IEnumerable<Actor>> GetSearchSuggestionsAsync(string query);
        Task<IEnumerable<Actor>> SearchActorsAsync(string query);
        Task<int> GetActorPositionAsync(Guid actorId);
        void Add(Actor actor);
        void Update(Actor actor);
        void Delete(Actor actor);
    }
}
