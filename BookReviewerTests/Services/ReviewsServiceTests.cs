using System.Collections.Generic;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;
using BookReviewerRestApi.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace BookReviewerTests.Services;

public class ReviewsServiceTests
{
    [Theory]
    [InlineData(1,1,4,2)]
    [InlineData(4,4,4,4)]
    [InlineData(1,1,1,1)]
    [InlineData(3,4,3,3.33)]

    public void Should_ReturnProperAverageRating_When_AverageRatingMethodIsCalled(int value1, int value2, int value3, double average)
    {
        var bookRepositoryStub = new Mock<IBookRepository>();
        var appUserRepositoryStub = new Mock<IAppUserRepository>();
        var reviewRepositoryStub = new Mock<IReviewRepository>();
        reviewRepositoryStub.Setup(repo => repo.GetReviewsByBook(It.IsAny<Book>())).Returns(new List<Review>()
        {
            new(){Rating = value1},
            new(){Rating = value2},
            new(){Rating = value3},
        });
        
        var service = new ReviewsService(bookRepositoryStub.Object,
            reviewRepositoryStub.Object,
            appUserRepositoryStub.Object);

        service.GetAverageBookRating("stub").Should().Be(average);
    }
}