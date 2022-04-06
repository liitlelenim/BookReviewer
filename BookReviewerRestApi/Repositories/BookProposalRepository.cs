using BookReviewerRestApi.DAL;
using BookReviewerRestApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewerRestApi.Repositories
{
    public class BookProposalRepository : IBookProposalRepository
    {
        private readonly AppDbContext _context;

        public BookProposalRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BookProposal> GetAll()
        {
            return _context.BookProposals;
        }

        public IEnumerable<BookProposal> GetPending()
        {
            return _context.BookProposals.Where(proposal => proposal.Status == BookProposalStatus.Pending)
                .Include(proposal => proposal.ProposedByUser);
        }

        public BookProposal GetById(int id)
        {
            BookProposal? proposal = _context.BookProposals.FirstOrDefault(proposal => proposal.Id == id);
            if (proposal == null)
            {
                throw new ArgumentException("Book proposal with given id does not exist");
            }

            return proposal;
        }

        public void Insert(BookProposal bookProposal)
        {
            _context.BookProposals.Add(bookProposal);
        }

        public void MarkForUpdate(BookProposal bookProposal)
        {
            _context.Entry(bookProposal).State = EntityState.Modified;
        }

        public void Remove(BookProposal bookProposal)
        {
            _context.BookProposals.Remove(bookProposal);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

    public interface IBookProposalRepository
    {
        public IEnumerable<BookProposal> GetAll();
        public IEnumerable<BookProposal> GetPending();
        public BookProposal GetById(int id);
        public void Insert(BookProposal bookProposal);
        public void MarkForUpdate(BookProposal bookProposal);
        public void Remove(BookProposal bookProposal);
        public void Save();
    }
}