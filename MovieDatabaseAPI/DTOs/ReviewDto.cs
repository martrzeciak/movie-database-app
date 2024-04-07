namespace MovieDatabaseAPI.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string ReviewContent { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
        public MemberDto? User { get; set; }
        public MovieDto? Movie { get; set; }
    }
}
