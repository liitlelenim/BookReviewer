namespace BookReviewerRestApi.DTO.Authentication
{
    public record AccountRegistrationDto(string Username, string Password, string PasswordConfirmation);
}
