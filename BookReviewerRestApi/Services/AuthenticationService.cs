using BookReviewerRestApi.DTO.Authentication;
using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;
using JWT.Algorithms;
using JWT.Builder;
using BCryptHashing = BCrypt.Net.BCrypt;

namespace BookReviewerRestApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IAppUserRepository _appUserRepository;

        public AuthenticationService(JwtOptions jwtOptions, IAppUserRepository appUserRepository)
        {
            _jwtOptions = jwtOptions;
            _appUserRepository = appUserRepository;
        }

        public string Login(LoginDto loginDto)
        {
            if (!_appUserRepository.ExistByUsername(loginDto.Username))
            {
                throw new ArgumentException("Incorrect username or password.");
            }

            AppUser user = _appUserRepository.GetByUsername(loginDto.Username);
            if (BCryptHashing.Verify(loginDto.Password, user.PasswordHash))
            {
                return CreateJwt(user.Username, user.Role.ToString());
            }

            throw new ArgumentException("Incorrect username or password.");
        }

        public void Register(AccountRegistrationDto registrationDto)
        {
            if (registrationDto.Password != registrationDto.PasswordConfirmation)
            {
                throw new ArgumentException("Password and confirmation password do not match.");
            }

            if (registrationDto.Username.Length < 3)
            {
                throw new ArgumentException("Username must be at least 3 characters long.");
            }

            if (_appUserRepository.ExistByUsername(registrationDto.Username))
            {
                throw new ArgumentException("Username is already taken.");
            }

            if (registrationDto.Password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            AppUser user = new AppUser()
            {
                Username = registrationDto.Username, PasswordHash = BCryptHashing.HashPassword(registrationDto.Password)
            };
            _appUserRepository.Insert(user);
            _appUserRepository.Save();
        }

        public string CreateJwt(string username, string role)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_jwtOptions.Secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                .AddClaim("username", username)
                .AddClaim("role", role)
                .Encode();
        }
    }

    public interface IAuthenticationService
    {
        public string Login(LoginDto loginDto);
        public void Register(AccountRegistrationDto registrationDto);
        public string CreateJwt(string username, string role);
    }
}