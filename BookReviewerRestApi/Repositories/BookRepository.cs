using BookReviewerRestApi.DAL;
using BookReviewerRestApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewerRestApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books;
        }

        public Book GetById(int id)
        {
            Book? book = _context.Books.Find(id);
            if (book == null)
            {
                throw new ArgumentException();
            }

            return book;
        }

        public Book GetByUri(string uri)
        {
            Book? book = _context.Books.SingleOrDefault(book => book.Uri == uri);
            if (book == null)
            {
                throw new ArgumentException();
            }

            return book;
        }

        public IEnumerable<Book> GetPaged(int page, int pageSize = 10)
        {
            return _context.Books.Skip((page - 1) * pageSize).Take(pageSize).Include(book => book.ReadBy);
        }

        public void Insert(Book book)
        {
            _context.Books.Add(book);
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
        }

        public void MarkForUpdate(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

    public interface IBookRepository
    {
        public IEnumerable<Book> GetAll();
        public Book GetById(int id);
        public Book GetByUri(string uri);
        public IEnumerable<Book> GetPaged(int page, int pageSize = 10);
        public void Insert(Book book);
        public void Remove(Book book);
        public void MarkForUpdate(Book book);
        public void Save();

    }
}
