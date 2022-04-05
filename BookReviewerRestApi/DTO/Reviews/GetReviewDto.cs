using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.DTO.Reviews;

public class GetReviewDto
{
    [Required] public string Uri { get; init; } = String.Empty;
    [Required] public int Rating { get; init; }
    [Required] public string Content { get; init; } = String.Empty;
    [Required] public string BookTitle { get; init; } = String.Empty;
    [Required] public string BookUri { get; init; } = String.Empty;
    [Required] public string AuthorUsername { get; init; } = String.Empty;
    [Required] public DateTime CreatedAt { get; init; }

}