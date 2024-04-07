using AutoMapper;
using MovieDatabaseAPI.Data.Repositories;
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

        public IActorRepository ActorRepository => new ActorRepository(_dataContext, _mapper);

        public ICommentRepository CommentRepository => new CommentRepository(_dataContext);

        public IGenreRepository GenreRepository => new GenreRepository(_dataContext);

        public IMovieRepository MovieRepository => new MovieRepository(_dataContext, _mapper);

        public IRatingRepository RatingRepository => new RatingRepository(_dataContext, _mapper);

        public IUserRepository UserRepository => new UserRepository(_dataContext, _mapper);

        public IReviewRepository ReviewRepository => new ReviewRepository(_dataContext);

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
