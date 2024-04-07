namespace MovieDatabaseAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IActorRepository ActorRepository { get; }
        ICommentRepository CommentRepository { get; }
        IGenreRepository GenreRepository { get; }
        IMovieRepository MovieRepository { get; }
        IRatingRepository RatingRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
