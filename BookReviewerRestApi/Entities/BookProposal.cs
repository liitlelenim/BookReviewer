using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.Entities
{
    public class BookProposal
    {
        [Key] public int Id { get; set; }
        [Required] public string BookTitle { get; set; } = String.Empty;
        public string BookAuthorFullName { get; set; } = String.Empty;
        public string BookDescription { get; set; } = String.Empty;
        public string BookCoverUrl { get; set; } = String.Empty;
        [Required] public BookProposalStatus Status { get; set; } = BookProposalStatus.Pending;
        [Required] public AppUser ProposedByUser { get; set; }
    }

    public enum BookProposalStatus
    {
        Pending,
        Rejected,
        Accepted
    }
}