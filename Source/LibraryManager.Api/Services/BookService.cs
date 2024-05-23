using AutoMapper;
using LibraryManager.Api.DTOs;
using LibraryManager.Data.Entities;
using LibraryManager.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Api.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddBookAsync(book);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> UpdateBookAsync(int bookId, BookDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return false;
            }

            _mapper.Map(bookDto, book);
            await _bookRepository.UpdateBookAsync(book);
            return true;
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return false;
            }

            await _bookRepository.DeleteBookAsync(bookId);
            return true;
        }
    }
}