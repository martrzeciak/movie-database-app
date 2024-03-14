namespace MovieDatabaseAPI.Helpers
{
    public class ActorParams : PaginationParams
    {
        public string Gender { get; set; } = string.Empty;
        public string OrderBy { get; set; } = "popularity";
    }
}
