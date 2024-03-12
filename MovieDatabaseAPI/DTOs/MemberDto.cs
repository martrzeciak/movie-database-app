namespace MovieDatabaseAPI.DTOs
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public DateTime Created { get; set; }
        public string Introduction { get; set; } = string.Empty;
        public string Localization { get; set; } = string.Empty;
        public List<UserImageDto> UserImages { get; set; } = new List<UserImageDto>();
    }
}