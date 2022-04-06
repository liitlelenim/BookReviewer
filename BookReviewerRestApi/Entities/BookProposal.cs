using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.Entities
{
    public class BookProposal
    {
        [Key] public int Id { get; set; } public string BookTitle { get; set; } = String.Empty;
        public string BookAuthorFullName { get; set; } = String.Empty;
        public string BookDescription { get; set; } = String.Empty;
        public string BookCoverUrl { get; set; } = String.Empty;
        public BookProposalStatus Status { get; set; } = BookProposalStatus.Pending;
        public AppUser ProposedByUser { get; set; }
    }

    public enum BookProposalStatus
    {
        Pending,
        Rejected,
        Accepted
    }
}