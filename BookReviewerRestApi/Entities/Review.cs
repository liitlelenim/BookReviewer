using System.ComponentModel.DataAnnotations;
using DEDrake;

namespace BookReviewerRestApi.Entities;

public class Review
{
    [Key] public int Id { get; set; }
    public string Uri { get; init; } = ShortGuid.NewGuid();
    [Range(1,5)] public int Rating { get; set; }
    [StringLength(10024)] public string Content { get; set; } = String.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public Book Book { get; set; }
    public AppUser User { get; set; }
}

