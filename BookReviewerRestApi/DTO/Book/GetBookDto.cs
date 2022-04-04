namespace BookReviewerRestApi.DTO.Book
{
    public class GetBookDto
    {
        public string Uri { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Author { get; set; } = String.Empty;
        public string CoverImageUrl { get; set; } = String.Empty;
        public int ReadByAmount { get; set; }
    }
}