using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
