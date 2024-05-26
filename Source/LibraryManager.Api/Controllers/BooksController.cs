using LibraryManager.Api.DTOs;
using LibraryManager.Api.Services;
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

        /// <summary>
        /// Retrieves all books from the repository
        /// </summary>
        /// <returns>A list of all stored books</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetBooksAsync()
        {
            var books = await _bookService.GetBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Retrieves the book with the specified ID
        /// </summary>
        /// <param name="id">ID of the requested book</param>
        /// <returns>Single book with the requested ID</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetBookAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Adds a book to the repository
        /// </summary>
        /// <param name="bookDto">A book to add</param>
        /// <returns>Response's status code (plus address of the created resource in case of success)</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateBookAsync(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await _bookService.CreateBookAsync(bookDto);

            return Created($"books/{createdBook.BookId}", createdBook);
        }

        /// <summary>
        /// Updates a book properties
        /// </summary>
        /// <param name="id">ID of the book to update</param>
        /// <param name="bookDto">Updated version of the book</param>
        /// <returns>Response's status code</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateBookAsync(int id, BookDto bookDto)
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the book with specified ID
        /// </summary>
        /// <param name="id">ID of the book to delete</param>
        /// <returns>Response's status code</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteBookAsync(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}