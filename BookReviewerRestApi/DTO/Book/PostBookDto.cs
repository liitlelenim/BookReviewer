using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.DTO
{
    public class PostBookDto
    {
        [Required, StringLength(1024)]
        public string Title { get; set; } = String.Empty;
        [StringLength(10024)]
        public string Description { get; set; } = String.Empty;
        [StringLength(1024)]
        public string Author { get; set; } = String.Empty;
        public string CoverImageUrl { get; set; } = String.Empty;
    }
}
