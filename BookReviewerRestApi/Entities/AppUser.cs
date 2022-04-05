using System.ComponentModel.DataAnnotations;
using DEDrake;

namespace BookReviewerRestApi.Entities
{
    public class AppUser
    {
        [Key] public int Id { get; init; }
        [Required] public string Uri { get; init; } = ShortGuid.NewGuid();
        [Required, StringLength(255)] public string Username { get; set; } = String.Empty;
        [Required] public string PasswordHash { get; set; } = String.Empty;
        [Required] public UserRole Role { get; set; } = UserRole.User;

        public List<Book> ReadBooks { get; set; } = new List<Book>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }

    public enum UserRole
    {
        User,
        Moderator,
        Administrator
    }

    public static class UserRoleString
    {
        public const string User = "User";
        public const string Moderator = "Moderator";
        public const string Administrator = "Administrator";
        public const string AdministratorOrModerator = "Administrator,Moderator";
    }
}