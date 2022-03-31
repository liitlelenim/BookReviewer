using BookReviewerRestApi.DTO;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;
using BookReviewerRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewerRestApi.Controllers
{
    [Route("api/book-proposals")]
    [ApiController]
    public class BookProposalsController : ControllerBase
    {
        private readonly IBookProposalRepository _bookProposalRepository;
        private readonly IBookProposingService _bookProposingService;

        public BookProposalsController(IBookProposalRepository bookProposalRepository, IBookProposingService bookProposingService)
        {
            _bookProposalRepository = bookProposalRepository;
            _bookProposingService = bookProposingService;
        }

        [HttpGet, Route("pending"), Authorize(Roles = UserRoleString.AdministratorOrModerator)]
        public ActionResult<IEnumerable<GetBookProposalDto>> GetPendingBookProposals()
        {
            return Ok(_bookProposalRepository.GetPending().Select(proposal => new GetBookProposalDto
            {
                Id = proposal.Id,
                BookTitle = proposal.BookTitle,
                BookAuthorFullName = proposal.BookAuthorFullName,
                BookCoverUrl = proposal.BookCoverUrl,
                BookDescription = proposal.BookDescription,
                ProposedByUsername = proposal.ProposedByUser.Username
            }));
        }

        [HttpPost, Authorize]
        public ActionResult PostBookProposal([FromBody] PostBookProposalDto postBookProposalDto)
        {
            try
            {

                _bookProposingService.AddBookProposal(postBookProposalDto, User.FindFirst("username").Value);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("{id:int}/accept"), Authorize(Roles = UserRoleString.AdministratorOrModerator)]
        public ActionResult AcceptBookProposal(int id, [FromBody] PostBookDto postBookDto)
        {
            try
            {
                _bookProposingService.AcceptBookProposal(id, postBookDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("{id:int}/reject"), Authorize(Roles = UserRoleString.AdministratorOrModerator)]
        public ActionResult AcceptBookProposal(int id)
        {
            try
            {
                _bookProposingService.RejectBookProposal(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
