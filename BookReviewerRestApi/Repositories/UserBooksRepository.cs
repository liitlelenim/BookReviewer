using BookReviewerRestApi.DAL;
using BookReviewerRestApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewerRestApi.Repositories;

public class UserBooksRepository : IUserBooksRepository
{
    private readonly AppDbContext _context;

    public UserBooksRepository(AppDbContext context)
    {
        _context = context;
    }

    public AppUser GetUserByIdWithBooks(int id)
    {
        AppUser? user = _context.AppUsers
            .Include(user => user.ReadBooks)
            .SingleOrDefault(user => user.Id == id);
        if (user is null)
        {
            throw new ArgumentException("User with given id does not exist.");
        }

        return user;
    }

    public AppUser GetUserByUsernameWithBooks(string username)
    {
        AppUser? user = _context.AppUsers.SingleOrDefault(user => user.Username == username);
        if (user is null)
        {
            throw new ArgumentException("User with given username does not exist.");
        }

        _context.Entry(user)
            .Collection(u => u.ReadBooks)
            .Load();
        return user;
    }

    public void AddBookToUserCollection(AppUser user, string bookUri)
    {
        Book? bookToAdd = _context.Books.SingleOrDefault(book => book.Uri == bookUri);
        if (bookToAdd is null)
        {
            throw new ArgumentException("Book with given uri does not exist.");
        }

        user.ReadBooks.Add(bookToAdd);
    }

    public void RemoveBookFromUserCollection(AppUser user, string bookUri)
    {
        Book? bookToRemove = _context.Books.SingleOrDefault(book => book.Uri == bookUri);
        if (bookToRemove is null)
        {
            throw new ArgumentException("Book with given uri does not exist.");
        }

        user.ReadBooks.Remove(bookToRemove);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}

public interface IUserBooksRepository
{
    public AppUser GetUserByIdWithBooks(int id);
    public AppUser GetUserByUsernameWithBooks(string username);
    public void AddBookToUserCollection(AppUser user, string bookUri);
    public void RemoveBookFromUserCollection(AppUser user, string bookUri);
    public void Save();
}