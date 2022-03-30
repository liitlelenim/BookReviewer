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
                _context.SaveChanges();
            }
        }
    }
}
