using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(Guid id);
    }
}
