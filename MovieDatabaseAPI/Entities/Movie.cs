namespace MovieDatabaseAPI.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public int ReleaseDate { get; set; }
        public int DurationInMinutes { get; set; }
        public string Director { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public Poster Poster { get; set; } = null!;

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}
