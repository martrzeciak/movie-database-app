using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByUserNameAsync(string username);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<PagedList<MemberDto>> GetMembersAsync(PaginationParams paginationParams);
        void Update(User user);
        Task<bool> SaveAllAsync();
    }
}
