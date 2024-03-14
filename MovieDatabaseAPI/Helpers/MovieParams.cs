namespace MovieDatabaseAPI.Helpers
{
    public class MovieParams : PaginationParams
    {
        public string ReleaseDate { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public string OrderBy { get; set; } = "popularity";
    }
}
