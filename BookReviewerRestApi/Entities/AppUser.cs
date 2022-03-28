using DEDrake;
using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.Entities
{
    public class AppUser
    {
        [Key]
        public int Id { get; init; }
        [Required]
        public string Uri { get; init; } = ShortGuid.NewGuid();
        [Required, StringLength(255)]
        public string Username { get; set; } = String.Empty;
        [Required]
        public string PasswordHash { get; set; } = String.Empty;
        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<Book> ReadBooks = new List<Book>();

    }
    public enum UserRole
    {
        User,
        Moderator,
        Administrator
    }
}
