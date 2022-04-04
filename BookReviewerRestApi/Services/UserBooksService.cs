using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;

namespace BookReviewerRestApi.Services;

public class UserBooksService : IUserBooksService
{
    private readonly UserBooksRepository _userBooksRepository;

    public UserBooksService(UserBooksRepository userBooksRepository)
    {
        _userBooksRepository = userBooksRepository;
    }


    public void AddBookToRead(string username, string bookUri)
    {
        AppUser user = _userBooksRepository.GetUserByUsernameWithBooks(username);
        _userBooksRepository.AddBookToUserCollection(user, bookUri);
        _userBooksRepository.Save();
    }

    public void RemoveBookFromRead(string username, string bookUri)
    {
        AppUser user = _userBooksRepository.GetUserByUsernameWithBooks(username);
        _userBooksRepository.RemoveBookFromUserCollection(user, bookUri);
        _userBooksRepository.Save();
    }
}

public interface IUserBooksService
{
    public void AddBookToRead(string username, string bookUri);
    public void RemoveBookFromRead(string username, string bookUri);
}