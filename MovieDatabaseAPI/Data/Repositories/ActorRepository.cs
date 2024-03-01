using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ActorRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorDto>> GetActors()
        {
            var actors = await _dataContext.Actors
                .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return actors;
        }

        public async Task<Actor?> GetActor(Guid id)
        {
            var actor = await _dataContext.Actors
                .Include(i => i.ActorImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }
    }
}
