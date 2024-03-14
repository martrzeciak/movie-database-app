namespace MovieDatabaseAPI.Entities
{
    public class ActorRating
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; } = null!;

        public int Rating { get; set; }
    }
}
