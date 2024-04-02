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

        public ICollection<Poster> Posters { get; set; } = new List<Poster>();
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
