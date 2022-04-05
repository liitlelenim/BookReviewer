namespace BookReviewerRestApi.DTO.Book
{
    public class GetBookDto
    {
        public string Uri { get; init; } = String.Empty;
        public string Title { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public string Author { get; init; } = String.Empty;
        public string CoverImageUrl { get; init; } = String.Empty;
        public int ReadByAmount { get; init; }
        public double AverageRating { get; init; }
        
    }
}