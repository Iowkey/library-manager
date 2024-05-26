using LibraryManager.Data.DataContext;
using LibraryManager.Data.Entities;
using LibraryManager.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LibraryManager.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        private readonly IDatabaseOperations _databaseOperations;

        public BookRepository(LibraryContext context, IDatabaseOperations databaseOperations)
        {
            _context = context;
            _databaseOperations = databaseOperations;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _databaseOperations.GetBooksAsync(1, int.MaxValue, null, "BookId", "ASC");
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            if (!IsValidBook(book))
            {
                throw new ArgumentException("Invalid book entity. Quantity can't be negative, and Publication year must be greater than 1000.");
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (!IsValidBook(book))
            {
                throw new ArgumentException("Invalid book entity. Quantity can't be negative, and Publication year must be greater than 1000.");
            }

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
        private bool IsValidBook(Book book)
        {
            return book.Quantity >= 0 && book.PublicationYear > 1000;
        }

    }
}
