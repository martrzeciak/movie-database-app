using AutoMapper;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IActorRepository ActorRepository => throw new NotImplementedException();

        public ICommentRepository CommentRepository => throw new NotImplementedException();

        public IGenreRepository GenreRepository => throw new NotImplementedException();

        public IMovieRepository MovieRepository => throw new NotImplementedException();

        public IRatingRepository RatingRepository => throw new NotImplementedException();

        public IUserRepository UserRepository => throw new NotImplementedException();

        public async Task<bool> Complete()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _dataContext.ChangeTracker.HasChanges();
        }
    }
}
