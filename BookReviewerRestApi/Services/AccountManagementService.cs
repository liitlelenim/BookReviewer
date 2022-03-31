using BookReviewerRestApi.Entities;
using BookReviewerRestApi.Repositories;

namespace BookReviewerRestApi.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IAppUserRepository _appUserRepository;

        public AccountManagementService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public void RemoveAccount(string username)
        {
            AppUser userToRemove = _appUserRepository.GetByUsername(username);
            _appUserRepository.Remove(userToRemove.Id);
            _appUserRepository.Save();
        }

        public void SetAccountRole(string username, UserRole role)
        {
            AppUser user = _appUserRepository.GetByUsername(username);
            user.Role = role;
            _appUserRepository.MarkForUpdate(user);
            _appUserRepository.Save();
        }
    }

    public interface IAccountManagementService
    {
        public void RemoveAccount(string username);
        public void SetAccountRole(string username, UserRole role);

    }
}
