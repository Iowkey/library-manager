using LibraryManager.Api.DTOs;
using LibraryManager.Api.Services;
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
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostBook(BookDto bookDto)
        {
            var createdBook = await _bookService.CreateBookAsync(bookDto);
            return CreatedAtRoute("GetBook", new { id = createdBook.BookId }, createdBook);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> PutBook(int id, BookDto bookDto)
        {
            if (id != bookDto.BookId) return BadRequest();
            var result = await _bookService.UpdateBookAsync(id, bookDto);
            if (!result) return NotFound();
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result) return NotFound();
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}