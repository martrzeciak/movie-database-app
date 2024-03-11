using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(PaginationParams paginationParams)
        {
            var query = _dataContext.Users.AsQueryable();

            return await PagedList<MemberDto>.CreateAsync(
                query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
                paginationParams.PageNumber,
                paginationParams.PageSize);
        }

        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            var user = await _dataContext.Users
                .Include(i => i.UserImage)
                .FirstOrDefaultAsync(x => x.UserName == username);

            return user;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _dataContext.Users
                .Include(i => i.UserImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public void Update(User user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

    }
}
