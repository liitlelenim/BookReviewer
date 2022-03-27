using Microsoft.EntityFrameworkCore;

namespace BookReviewerRestApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
