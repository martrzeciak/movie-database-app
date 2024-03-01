using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<ActorDto>> GetActors();
        Task<Actor?> GetActor(Guid id);
    }
}
