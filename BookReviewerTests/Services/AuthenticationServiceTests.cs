using System;
using System.Collections.Generic;
using BookReviewerRestApi;
using BookReviewerRestApi.DTO.Authentication;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;
using BookReviewerRestApi.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace BookReviewerTests.Services
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public void Should_ThrowArgumentException_When_LoginUsernameDoesNotExist()
        {
            var optionsStub = new JwtOptions();
            var repositoryStub = new Mock<IAppUserRepository>();

            repositoryStub.Setup(repo => repo.ExistByUsername(It.IsAny<string>())).Returns(false);
            var service = new AuthenticationService(optionsStub, repositoryStub.Object);

            Action loginAction = () => service.Login(new LoginDto("NonExistentUsername", "Password"));
            loginAction.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void Should_ThrowArgumentException_When_UserExistsButPasswordIsIncorrect()
        {
            var optionsStub = new JwtOptions();
            var repositoryStub = new Mock<IAppUserRepository>();
            var appUserStub = new AppUser
                { Id = 1, Username = "TestUser", PasswordHash = BCrypt.Net.BCrypt.HashPassword("testPassword123") };

            repositoryStub.Setup(repo => repo.ExistByUsername(It.IsAny<string>())).Returns(true);
            repositoryStub.Setup(repo => repo.GetByUsername(It.IsAny<String>())).Returns(appUserStub);
            var service = new AuthenticationService(optionsStub, repositoryStub.Object);

            Action loginAction = () => service.Login(new LoginDto("TestUser", "IncorrectPassword"));
            loginAction.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void Should_ThrowArgumentException_When_AccountRegistrationDtoDoesNotPassServiceValidation()
        {
            var optionsStub = new JwtOptions();
            var repositoryStub = new Mock<IAppUserRepository>();
            repositoryStub.Setup(repo => repo.ExistByUsername(It.IsAny<string>())).Returns(false);

            repositoryStub.Setup(repo => repo.ExistByUsername("ExistingUser")).Returns(true);
            var service = new AuthenticationService(optionsStub, repositoryStub.Object);

            List<AccountRegistrationDto> InvalidDto = new()
            {
                new AccountRegistrationDto("TestUser", "MyPassword", "MisMatchedPassword"), //Mismatched passwords
                new AccountRegistrationDto("us", "CorrectPassword123", "CorrectPassword123"), //Too short username
                new AccountRegistrationDto("ExistingUser", "CorrectPassword123",
                    "CorrectPassword123"), //Username is taken 
                new AccountRegistrationDto("TestUser", "short", "short") //Too short password
            };
            foreach (AccountRegistrationDto dto in InvalidDto)
            {
                Action registrationAction = () => service.Register(dto);
                registrationAction.Should().ThrowExactly<ArgumentException>();
            }
        }

        [Fact]
        public void Should_CreateAccountWithoutAnyExceptions_When_GivenDtoIsValid()
        {
            var optionsStub = new JwtOptions();
            var repositoryStub = new Mock<IAppUserRepository>();
            repositoryStub.Setup(repo => repo.ExistByUsername(It.IsAny<string>())).Returns(false);

            var service = new AuthenticationService(optionsStub, repositoryStub.Object);
            Action registrationAction = () =>
            {
                service.Register(
                    new AccountRegistrationDto("ValidUsername", "ValidPassword123", "ValidPassword123"));
            };
            registrationAction.Should().NotThrow();
        }
    }
}