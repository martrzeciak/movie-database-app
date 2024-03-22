namespace MovieDatabaseAPI.Entities
{
    public class Poster
    {
        public Guid Id { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public bool IsMain { get; set; }


        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
