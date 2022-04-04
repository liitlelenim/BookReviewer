using BookReviewerRestApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewerRestApi.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookProposal> BookProposals { get; set; }
        
        public DbSet<Review> Reviews { get; set; }
    }
}
