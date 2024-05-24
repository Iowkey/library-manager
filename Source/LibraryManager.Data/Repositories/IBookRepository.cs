using LibraryManager.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Data.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
