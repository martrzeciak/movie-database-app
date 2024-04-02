namespace MovieDatabaseAPI.DTOs
{
    public class ActorImageDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
