using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementation
{

    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBook(Book bookDto)
        {
            var book = await _context.Books.FindAsync(bookDto.Id); // ✅ Fetch in the same context

            if (book != null)
            {
                _context.Books.Remove(book); // ✅ Remove existing tracked entity
                await _context.SaveChangesAsync();
            }
        }
        //public async Task DeleteBook(Book book)
        //{
        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<Book?> GetBookById(int id) => await _context.Books.FindAsync(id);

        public async Task<IEnumerable<Book>> GetBooks() => await _context.Books.Include(a => a.Genre).ToListAsync();
    }
}
