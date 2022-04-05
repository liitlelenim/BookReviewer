using BookReviewerRestApi.DAL;
using BookReviewerRestApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewerRestApi.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Review> GetReviewsByBook(Book book)
    {
        _context.Entry(book).Collection(b => b.Reviews).Load();
        _context.Reviews.Include(review => review.User);
        return book.Reviews;
    }

    public Review GetReviewByUri(string uri)
    {
        Review? review = _context.Reviews.SingleOrDefault(r => r.Uri == uri);
        if (review is null)
        {
            throw new ArgumentException("Review with given uri does not exist.");
        }
        _context.Entry(review).Reference(r => r.User);
        return review;
    }

    public void AddReviewToBook(Book book, Review review, AppUser author)
    {
        _context.Entry(author).Collection(user => user.ReadBooks).Load();
        if (!author.ReadBooks.Contains(book))
        {
            throw new ArgumentException("Author book collection does not contain given book");
        } 
        _context.Entry(book).Collection(b =>b.Reviews ).Load();
        _context.Reviews.Include(r => r.User);
        if (book.Reviews.SingleOrDefault(r => r.User == author) is not null)
        {
            throw new ArgumentException("This book has been already reviewed by given user.");
        }
        book.Reviews.Add(review);
    }

    public void RemoveReview(Review review)
    {
        _context.Reviews.Remove(review);
    }

    public void MarkForUpdate(Review review)
    {
        _context.Entry(review).State = EntityState.Modified;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}

public interface IReviewRepository
{
    public IEnumerable<Review> GetReviewsByBook(Book book);
    public Review GetReviewByUri(string uri);
    public void AddReviewToBook(Book book, Review review, AppUser user);
    public void RemoveReview(Review review);
    public void MarkForUpdate(Review review);
    public void Save();
}