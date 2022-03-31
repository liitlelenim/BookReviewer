using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewerRestApi.Controllers
{
    [Route("api/account-management")]
    [ApiController]
    public class AccountsManagementController : ControllerBase
    {
        private readonly IAccountManagementService _accountManagementService;

        public AccountsManagementController(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        [HttpPut, Route("{username}/role"), Authorize(Roles = UserRoleString.Administrator)]
        public ActionResult SetUserRole(string username, [FromBody] UserRole role)
        {
            try
            {
                _accountManagementService.SetAccountRole(username, role);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete, Route("{username}"), Authorize(Roles = UserRoleString.Administrator)]
        public ActionResult RemoveAnotherUserAccount(string username)
        {
            try
            {
                _accountManagementService.RemoveAccount(username);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete, Authorize]
        public ActionResult RemoveYourAccount()
        {
            try
            {
                _accountManagementService.RemoveAccount(User!.FindFirst("username")!.Value);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}