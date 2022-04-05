using System.Collections;
using BookReviewerRestApi.DTO.Reviews;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;

namespace BookReviewerRestApi.Services;

public class ReviewsService : IReviewsService
{
    private readonly IBookRepository _bookRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IAppUserRepository _appUserRepository;
    
    public ReviewsService(IBookRepository bookRepository, IReviewRepository reviewRepository, IAppUserRepository appUserRepository)
    {
        _bookRepository = bookRepository;
        _reviewRepository = reviewRepository;
        _appUserRepository = appUserRepository;
    }


    public GetReviewDto GetReviewByUri(string uri)
    {
        Review review = _reviewRepository.GetReviewByUri(uri);
        return new GetReviewDto
        {
            Uri = review.Uri,
            Rating = review.Rating,
            Content = review.Content,
            BookTitle = review.Book!.Title,
            BookUri = review.Book!.Uri,
            AuthorUsername = review.User!.Username,
            CreatedAt = review.CreatedAt
        };
    }

    public IEnumerable<GetReviewDto> GetBookReviews(string bookUri)
    {
        Book book = _bookRepository.GetByUri(bookUri);
        IEnumerable<Review> reviews = _reviewRepository.GetReviewsByBook(book);

        return reviews.Select(review => new GetReviewDto
        {
            Uri = review.Uri,
            Rating = review.Rating,
            Content = review.Content,
            BookTitle = review.Book!.Title,
            BookUri = review.Book!.Uri,
            AuthorUsername = review.User!.Username,
            CreatedAt = review.CreatedAt
        });
    }

    public GetReviewDto AddReviewToTheBook(string username, string bookUri, PostReviewDto dto)
    {
        AppUser user = _appUserRepository.GetByUsername(username);
        Book book = _bookRepository.GetByUri(bookUri);
        Review createdReview = new Review
        {
            Content = dto.Content,
            Rating = dto.Rating
        };
        _reviewRepository.AddReviewToBook(book,createdReview,user);
        _reviewRepository.Save();
        return new GetReviewDto
        {
            Uri = createdReview.Uri,
            Rating = createdReview.Rating,
            Content = createdReview.Content,
            BookTitle = createdReview.Book!.Title,
            BookUri = createdReview.Book!.Uri,
            AuthorUsername = createdReview.User!.Username,
            CreatedAt = createdReview.CreatedAt
        };
    }

    public double GetAverageBookRating(string bookUri)
    {
        Book book = _bookRepository.GetByUri(bookUri);
        IEnumerable<Review> reviews  = _reviewRepository.GetReviewsByBook(book);
        if (reviews.Any())
        {
            return Math.Round(reviews.Select(review => review.Rating).Average(), 2);
        }

        return 0;
    }

    public void RemoveReview(string username, string bookUri)
    {
        Review review = _reviewRepository.GetReviewByUsernameAndBookUri(username,bookUri);
        _reviewRepository.RemoveReview(review);
        _reviewRepository.Save();
    }
}

public interface IReviewsService
{
    public GetReviewDto GetReviewByUri(string uri);
    public IEnumerable<GetReviewDto> GetBookReviews(string bookUri);
    public GetReviewDto AddReviewToTheBook(string username, string bookUri, PostReviewDto dto);
    public double GetAverageBookRating(string bookUri);
    public void RemoveReview(string username,string bookUri);
}