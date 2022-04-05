using System;
using BookReviewerRestApi.Controllers;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookReviewerTests.Controllers;

public class AccountManagementControllerTests
{
    [Fact]
    public void Should_ReturnNoContent_When_ProperSetRoleRequestIsGiven()
    {
        var serviceStub = new Mock<IAccountManagementService>();
        var controller = new AccountsManagementController(serviceStub.Object);

        controller.SetUserRole("PersonToPromote", UserRole.Administrator).Should().BeOfType<NoContentResult>();
    }
    [Fact]
    public void Should_ReturnNoBadRequest_When_ArgumentExceptionsOccursOnSettingRole()
    {
        var serviceStub = new Mock<IAccountManagementService>();
        serviceStub.Setup(service => service.
            SetAccountRole(It.IsAny<string>(), It.IsAny<UserRole>())).Throws<ArgumentException>();
        
        var controller = new AccountsManagementController(serviceStub.Object);

        controller.SetUserRole("InvalidUsername", UserRole.Administrator).Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void Should_ReturnNoContent_When_ProperRemoveUserRequestIsGiven()
    {
        var serviceStub = new Mock<IAccountManagementService>();

        var controller = new AccountsManagementController(serviceStub.Object);

        controller.RemoveAnotherUserAccount("PersonToRemove").Should().BeOfType<NoContentResult>();
    }
    [Fact]
    public void Should_ReturnBadRequest_When_InvalidRemoveUserRequestIsGiven()
    {
        var serviceStub = new Mock<IAccountManagementService>();
        serviceStub.Setup(service => service.RemoveAccount(It.IsAny<string>())).Throws<ArgumentException>();
        
        var controller = new AccountsManagementController(serviceStub.Object);

        controller.RemoveAnotherUserAccount("InvalidUsername").Should().BeOfType<BadRequestObjectResult>();
        
    }
    
}