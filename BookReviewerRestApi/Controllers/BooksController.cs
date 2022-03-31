using BookReviewerRestApi.DTO.Book;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewerRestApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator,Administrator")]

        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_bookRepository.GetAll());
        }

        [HttpGet, Route("page/{pageIndex:int}")]
        public ActionResult<IEnumerable<GetBookDto>> GetBookByPage(int pageIndex = 1)
        {
            IEnumerable<GetBookDto> pageContent = _bookRepository.GetPaged(pageIndex).Select(book => new GetBookDto
            {
                Uri = book.Uri,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                CoverImageUrl = book.CoverImageUrl,
                ReadByAmount = book.ReadBy.ToList().Count
            });

            return pageContent.Any() ? Ok(pageContent) : NotFound($"Page {pageIndex} is empty.");
        }
    }
}
