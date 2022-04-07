using BookReviewerRestApi.DAL;
using BookReviewerRestApi.Entities;

namespace BookReviewerRestApi.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.AppUsers;
        }

        public AppUser GetById(int id)
        {
            AppUser? user = _context.AppUsers.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                throw new ArgumentException("User with given id does not exist.");
            }

            return user;
        }

        public AppUser GetByUri(string uri)
        {
            AppUser? user = _context.AppUsers.FirstOrDefault(user => user.Uri == uri);
            if (user == null)
            {
                throw new ArgumentException("User with given uri does not exist.");
            }

            return user;
        }

        public AppUser GetByUsername(string username)
        {
            AppUser? user = _context.AppUsers.FirstOrDefault(user => user.Username == username);
            if (user == null)
            {
                throw new ArgumentException("User with given username does not exist.");
            }

            return user;
        }

        public bool ExistByUsername(string username)
        {
            return _context.AppUsers.FirstOrDefault(user => user.Username == username) != null;
        }

        public void Insert(AppUser user)
        {
            _context.AppUsers.Add(user);
        }

        public void Remove(int id)
        {
            _context.AppUsers.Remove(GetById(id));
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }

    public interface IAppUserRepository
    {
        public IEnumerable<AppUser> GetAll();
        public AppUser GetById(int id);
        public AppUser GetByUri(string uri);
        public AppUser GetByUsername(string username);
        public bool ExistByUsername(string username);
        public void Insert(AppUser user);
        public void Remove(int id);
        public void Save();
    }
}