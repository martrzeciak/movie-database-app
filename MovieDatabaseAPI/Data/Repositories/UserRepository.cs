using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            var user = await _dataContext.Users
                .Include(i => i.UserImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }
    }
}
