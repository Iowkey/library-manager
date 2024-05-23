using LibraryManager.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Api.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int bookId);
        Task<BookDto> CreateBookAsync(BookDto bookDto);
        Task<bool> UpdateBookAsync(int bookId, BookDto bookDto);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
