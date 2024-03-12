namespace MovieDatabaseAPI.Entities
{
    public class UserImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public bool IsMain { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
