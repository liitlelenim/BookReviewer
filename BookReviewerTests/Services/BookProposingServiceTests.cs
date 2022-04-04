using BookReviewerRestApi.DTO;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;
using BookReviewerRestApi.Services;
using FluentAssertions;
using Moq;
using System;
using BookReviewerRestApi.DTO.BookProposal;
using Xunit;

namespace BookReviewerTests.Services
{
    public class BookProposingServiceTests
    {
        [Fact]
        public void Should_CreateBookProposal_When_GivenDtoIsValid()
        {
            var userStub = new AppUser()
            {
                Id = 1,
                PasswordHash = "stub",
                Username = "stub"
            };

            var postBookProposalDtoStub = new PostBookProposalDto()
            {
                BookAuthorFullName = "StubTitle",
            };

            var bookProposingRepositoryStub = new Mock<IBookProposalRepository>();
            var bookRepositoryStub = new Mock<IBookRepository>();
            var userRepositoryStub = new Mock<IAppUserRepository>();

            userRepositoryStub.Setup(repo => repo.GetByUsername(It.IsAny<string>())).Returns(userStub);

            var service = new BookProposingService
            (bookProposingRepositoryStub.Object,
                bookRepositoryStub.Object,
                userRepositoryStub.Object);
            Action addProposalAction = () => service.AddBookProposal(postBookProposalDtoStub, "stub");
            addProposalAction.Should().NotThrow();
        }

        [Fact]
        public void Should_ThrowArgumentException_When_GivenBookProposalIsAlreadyResolved()
        {
            var userStub = new AppUser()
            {
                Id = 1,
                PasswordHash = "stub",
                Username = "stub",
            };

            var resolvedBookProposalStub = new BookProposal()
            {
                Id = 1,
                BookTitle = "StubTitle",
                ProposedByUser = userStub,
                Status = BookProposalStatus.Accepted
            };

            var bookProposingRepositoryStub = new Mock<IBookProposalRepository>();
            var bookRepositoryStub = new Mock<IBookRepository>();
            var userRepositoryStub = new Mock<IAppUserRepository>();

            bookProposingRepositoryStub.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(resolvedBookProposalStub);

            var service = new BookProposingService
            (bookProposingRepositoryStub.Object,
                bookRepositoryStub.Object,
                userRepositoryStub.Object);

            Action acceptProposalAction = () => service.AcceptBookProposal(1, new());
            Action rejectProposalAction = () => service.RejectBookProposal(1);

            acceptProposalAction.Should().ThrowExactly<ArgumentException>();
            rejectProposalAction.Should().ThrowExactly<ArgumentException>();

        }
    }
}
