namespace BookReviewerRestApi
{
    public class JwtOptions
    {
        public string Secret { get; init; }
        public string Audience { get; init; }
        public string Issuer { get; init; }
        public string Subject { get; init; }

        public JwtOptions(IConfiguration configuration)
        {
            Secret = configuration["Jwt:Secret"];
            Audience = configuration["Jwt:Audience"];
            Issuer = configuration["Jwt:Issuer"];
            Subject = configuration["Jwt:Subject"];
        }
    }
}
