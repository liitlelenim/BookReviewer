namespace BookReviewerRestApi
{
    public class JwtOptions
    {
        public string Secret { get; init; }

        public JwtOptions(IConfiguration configuration)
        {
            Secret = configuration["Jwt:Secret"];
        }

        public JwtOptions()
        {
        }
    }
}