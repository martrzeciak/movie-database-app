namespace MovieDatabaseAPI.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string CommentContent { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }
        public bool IsEdited { get; set; }
        public MemberDto? User { get; set; }
    }
}
