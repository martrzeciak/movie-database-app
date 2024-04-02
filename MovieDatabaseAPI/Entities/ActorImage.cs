namespace MovieDatabaseAPI.Entities
{
    public class ActorImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string PublicId { get; set; } = string.Empty;
        public bool IsMain { get; set; }

        public Guid ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
    }
}