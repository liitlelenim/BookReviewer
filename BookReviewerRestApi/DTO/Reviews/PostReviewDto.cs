using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.DTO.Reviews;

public class PostReviewDto
{
    [Required, Range(1, 5)] public int Rating { get; set; }
    [StringLength(10024)] public string Content { get; set; } = String.Empty;
}