namespace MovieDatabaseAPI.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string ReviewContent { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.Now;


        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
