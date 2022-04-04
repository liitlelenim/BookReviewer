namespace BookReviewerRestApi.DTO.BookProposal
{
    public class GetBookProposalDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = String.Empty;
        public string BookAuthorFullName { get; set; } = String.Empty;
        public string BookDescription { get; set; } = String.Empty;
        public string BookCoverUrl { get; set; } = String.Empty;

        public string ProposedByUsername { get; set; } = String.Empty;
    }
}
