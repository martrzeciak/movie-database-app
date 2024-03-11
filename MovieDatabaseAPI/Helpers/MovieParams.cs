namespace MovieDatabaseAPI.Helpers
{
    public class MovieParams : PaginationParams
    {
        public int ReleaseDate { get; set; }
        public string? Genre { get; set; }
    }
}
