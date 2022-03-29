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
    }
}
