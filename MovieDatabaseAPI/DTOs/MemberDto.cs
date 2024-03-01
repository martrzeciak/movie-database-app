namespace MovieDatabaseAPI.DTOs
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
