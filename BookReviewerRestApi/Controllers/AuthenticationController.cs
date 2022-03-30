using BookReviewerRestApi.DTO.Authentication;
using BookReviewerRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewerRestApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost, Route("register")]
        public IActionResult RegisterAccount([FromBody] AccountRegistrationDto registrationDto)
        {
            try
            {
                _authenticationService.Register(registrationDto);
                return Ok();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {

            try
            {
                return Ok(_authenticationService.Login(loginDto));
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
