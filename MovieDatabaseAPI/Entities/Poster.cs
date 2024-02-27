namespace MovieDatabaseAPI.Entities
{
    public class Poster
    {
        public Guid Id { get; set; }
        public string PosterUrl { get; set; } = null!;

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
