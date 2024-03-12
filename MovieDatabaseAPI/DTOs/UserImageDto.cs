namespace MovieDatabaseAPI.DTOs
{
    public class UserImageDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
