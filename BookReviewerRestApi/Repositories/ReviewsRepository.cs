using BookReviewerRestApi.DAL;
using BookReviewerRestApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewerRestApi.Repositories;

public class ReviewRepository : IReviewsRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Review> GetReviewsByBook(Book book)
    {
        _context.Entry(book).Collection(b => b.Reviews).Load();
        return book.Reviews;
    }

    public Review GetReviewByUri(string uri)
    {
        Review? review = _context.Reviews.SingleOrDefault(r => r.Uri == uri);
        if (review is null)
        {
            throw new ArgumentException("Review with given uri does not exist.");
        }

        return review;
    }

    public void AddReviewToBook(Book book, Review review)
    {
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

public interface IReviewsRepository
{
    public IEnumerable<Review> GetReviewsByBook(Book book);
    public Review GetReviewByUri(string uri);
    public void AddReviewToBook(Book book, Review review);
    public void RemoveReview(Review review);
    public void MarkForUpdate(Review review);
    public void Save();
}