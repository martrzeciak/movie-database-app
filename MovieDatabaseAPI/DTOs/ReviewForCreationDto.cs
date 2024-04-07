namespace MovieDatabaseAPI.DTOs
{
    public class ReviewForCreationDto
    {
        public int Rating { get; set; }
        public string ReviewContent { get; set; } = string.Empty;
    }
}
