using System.ComponentModel.DataAnnotations;

namespace BookReviewerRestApi.DTO.BookProposal
{
    public class PostBookProposalDto
    {
        [Required] public string BookTitle { get; set; } = String.Empty;
        public string BookDescription { get; set; } = String.Empty;
        public string BookAuthorFullName { get; set; } = String.Empty;

        public string BookCoverUrl { get; set; } = String.Empty;
    }
}