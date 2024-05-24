using LibraryManager.Api.DTOs;
using LibraryManager.Api.Services;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace LibraryManager.Api.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await _bookService.CreateBookAsync(bookDto);

            return Created($"books/{createdBook.BookId}", createdBook);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != bookDto.BookId)
            {
                return BadRequest();
            }

            var result = await _bookService.UpdateBookAsync(id, bookDto);
            if (!result)
            {
                return NotFound();
            }

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}