namespace MovieDatabaseAPI.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
