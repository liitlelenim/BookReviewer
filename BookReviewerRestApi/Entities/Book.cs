using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(1024)]
        public string Title { get; set; } = String.Empty;
        [StringLength(1024)]
        public string Author { get; set; } = String.Empty;

        public ICollection<AppUser> ReadBy { get; set; } = new List<AppUser>();
    }
}
