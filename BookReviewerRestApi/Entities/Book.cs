using System.ComponentModel.DataAnnotations;
using DEDrake;

namespace BookReviewerRestApi.Entities
{
    public class Book
    {
        [Key] public int Id { get; set; }
        public string Uri { get; init; } = ShortGuid.NewGuid();
        [StringLength(1024)] public string Title { get; set; } = String.Empty;
        [StringLength(10024)] public string Description { get; set; } = String.Empty;
        [StringLength(1024)] public string Author { get; set; } = String.Empty;
        public string CoverImageUrl { get; set; } = String.Empty;

        public IEnumerable<AppUser> ReadBy { get; set; } = new List<AppUser>();
        public List<Review> Reviews { get; set; } = new List<Review>();

    }
}