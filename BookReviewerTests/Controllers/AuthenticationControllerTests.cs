using System;
using BookReviewerRestApi.Controllers;
using BookReviewerRestApi.DTO.Authentication;
using BookReviewerRestApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookReviewerTests.Controllers
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public void Should_ReturnOk_When_AccountRegistrationIsSuccessful()
        {
            var authenticationServiceStub = new Mock<IAuthenticationService>();
            var controller = new AuthenticationController(authenticationServiceStub.Object);

            controller.RegisterAccount(new AccountRegistrationDto("ValidUsername", "ValidPassword", "ValidPassword"))
                .Should().BeOfType<OkResult>();
        }

        [Fact]
        public void Should_ReturnBadRequestObject_When_AccountRegistrationFailedDueToArgumentException()
        {
            var authenticationServiceStub = new Mock<IAuthenticationService>();
            authenticationServiceStub.Setup(repo => repo.Register(It.IsAny<AccountRegistrationDto>()))
                .Throws<ArgumentException>();
            var controller = new AuthenticationController(authenticationServiceStub.Object);


            controller.RegisterAccount(new AccountRegistrationDto("Invalid", "Err", "Roor"))
                .Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Should_ReturnOk_When_LoginIsSuccessful()
        {
            var authenticationServiceStub = new Mock<IAuthenticationService>();
            var controller = new AuthenticationController(authenticationServiceStub.Object);

            controller.Login(new LoginDto("ValidUsername", "ValidPassword"))
                .Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Should_ReturnBadRequestObject_When_LoginFailedDueToArgumentException()
        {
            var authenticationServiceStub = new Mock<IAuthenticationService>();
            authenticationServiceStub.Setup(repo => repo.Login(It.IsAny<LoginDto>())).Throws<ArgumentException>();
            var controller = new AuthenticationController(authenticationServiceStub.Object);


            controller.Login(new LoginDto("ValidUsername", "ValidPassword"))
                .Should().BeOfType<BadRequestObjectResult>();
        }
    }
}