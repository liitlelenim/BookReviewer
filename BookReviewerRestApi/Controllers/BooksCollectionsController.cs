using BookReviewerRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewerRestApi.Controllers;

[Route("/api/users")]
[ApiController]
public class BooksCollectionsController : ControllerBase
{
    private readonly IUserBooksService _userBooksService;

    public BooksCollectionsController(IUserBooksService userBooksService)
    {
        _userBooksService = userBooksService;
    }

    [Authorize]
    [HttpPost, Route("{username}/books-collection")]
    public ActionResult AddBookToUserCollection([FromBody] string bookUri, string username)
    {
        if (HttpContext.User.FindFirst("username")?.Value == username)
        {
            try
            {
                _userBooksService.AddBookToRead(username, bookUri);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }

            return NoContent();
        }

        return Forbid();
    }
    [Authorize]
    [HttpDelete, Route("{username}/books-collection")]
    public ActionResult RemoveBookFromUserCollection([FromBody] string bookUri, string username)
    {
        if (HttpContext.User.FindFirst("username")?.Value == username)
        {
            try
            {
                _userBooksService.RemoveBookFromRead(username, bookUri);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }

            return NoContent();
        }

        return Forbid();
    }
}