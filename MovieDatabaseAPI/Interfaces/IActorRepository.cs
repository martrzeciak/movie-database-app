using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetActors();
        Task<Actor?> GetActor(Guid id);
    }
}
