using BookReviewerRestApi.DTO;
using BookReviewerRestApi.DTO.BookProposal;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;

namespace BookReviewerRestApi.Services
{
    public class BookProposingService : IBookProposingService
    {
        private readonly IBookProposalRepository _bookProposalRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAppUserRepository _appUserRepository;

        public BookProposingService(IBookProposalRepository bookProposalRepository, IBookRepository bookRepository, IAppUserRepository appUserRepository)
        {
            _bookProposalRepository = bookProposalRepository;
            _bookRepository = bookRepository;
            _appUserRepository = appUserRepository;
        }

        public void AddBookProposal(PostBookProposalDto dto, string username)
        {
            BookProposal proposal = new BookProposal
            {
                BookTitle = dto.BookTitle,
                BookDescription = dto.BookDescription,
                BookAuthorFullName = dto.BookAuthorFullName,
                BookCoverUrl = dto.BookCoverUrl,
                ProposedByUser = _appUserRepository.GetByUsername(username)
            };
            _bookProposalRepository.Insert(proposal);
            _bookProposalRepository.Save();
        }
        public Book AcceptBookProposal(int bookProposalId, PostBookDto postBookDto)
        {
            BookProposal proposal = _bookProposalRepository.GetById(bookProposalId);
            if (proposal.Status != BookProposalStatus.Pending)
            {
                throw new ArgumentException("Given book proposal has already been resolved.");
            }

            Book book = new Book
            {
                Title = postBookDto.Title,
                Description = postBookDto.Description,
                Author = postBookDto.Author,
                CoverImageUrl = postBookDto.CoverImageUrl
            };
            _bookRepository.Insert(book);
            _bookRepository.Save();

            proposal.Status = BookProposalStatus.Accepted;
            _bookProposalRepository.MarkForUpdate(proposal);
            _bookProposalRepository.Save();

            return book;

        }
        public void RejectBookProposal(int bookProposalId)
        {
            BookProposal proposal = _bookProposalRepository.GetById(bookProposalId);
            if (proposal.Status != BookProposalStatus.Pending)
            {
                throw new ArgumentException("Given book proposal has already been resolved.");
            }

            proposal.Status = BookProposalStatus.Rejected;
            _bookProposalRepository.MarkForUpdate(proposal);
            _bookProposalRepository.Save();

        }
    }

    public interface IBookProposingService
    {
        public void AddBookProposal(PostBookProposalDto dto, string username);
        public Book AcceptBookProposal(int bookProposalId, PostBookDto postBookDto);
        public void RejectBookProposal(int bookProposalId);
    }
}
