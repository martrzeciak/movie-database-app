using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
