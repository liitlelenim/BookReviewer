using BookReviewerRestApi.DTO.Reviews;
using BookReviewerRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewerRestApi.Controllers;

[ApiController]
[Route("/api/")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewsService _reviewsService;

    public ReviewsController(IReviewsService reviewsService)
    {
        _reviewsService = reviewsService;
    }

    [HttpPost, Route("{username}/books-collection/{bookUri}/review")]
    [Authorize]
    public ActionResult<GetReviewDto> AddReview(string username, string bookUri, [FromBody] PostReviewDto dto)
    {
        if (HttpContext.User.FindFirst("username")?.Value != username)
        {
            return Forbid();
        }
        try
        {
            GetReviewDto getReviewDto = _reviewsService.AddReviewToTheBook(username, bookUri, dto);
            return Created(getReviewDto.Uri, getReviewDto);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete, Route("{username}/books-collection/{bookUri}/review")]
    [Authorize]
    public ActionResult RemoveReview(string username, string bookUri)
    {
        if (HttpContext.User.FindFirst("username")?.Value != username)
        {
            return Forbid();
        }
        try
        {
            _reviewsService.RemoveReview(username,bookUri);
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}