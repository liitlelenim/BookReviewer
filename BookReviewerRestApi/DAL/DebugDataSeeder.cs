using BookReviewerRestApi.Entities;

namespace BookReviewerRestApi.DAL
{
    public class DebugDataSeeder
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public DebugDataSeeder(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void Seed()
        {
            if (!_context.AppUsers.Any())
            {
                _context.AppUsers.Add(
                    new AppUser
                    {
                        Username = _configuration["Admin:Username"],
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(_configuration["Admin:Password"]),
                        Role = UserRole.Administrator
                    });
            }

            if (!_context.Books.Any())
            {
                _context.Books.AddRange(
                    new Book()
                    {
                        Title = "The Old Man and the Sea",
                        Author =  "Ernest Hemingway",
                        CoverImageUrl = "https://en.wikipedia.org/wiki/The_Old_Man_and_the_Sea#/media/File:Oldmansea.jpg",
                    },
                    new Book()
                    {
                        Title = "An Artist of the Floating World",
                        Author =  "Kazuo Ishiguro",
                        CoverImageUrl = "https://en.wikipedia.org/wiki/An_Artist_of_the_Floating_World#/media/File:ArtistOfTheFloatingWorld.jpg",
                    },
                    new Book()
                    {
                        Title = "The Lord of the Rings",
                        Author =  "John Ronald Reuel Tolkien",
                        CoverImageUrl = "https://en.wikipedia.org/wiki/The_Lord_of_the_Rings#/media/File:First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                    }
                    
            );
            }
            _context.SaveChanges();
        }
    }
}
