using LibraryManager.Api.Controllers;
using LibraryManager.Api.DTOs;
using LibraryManager.Api.Services;
using LibraryManager.UnitTests.Mocks;
using LibraryManager.UnitTests.TestData;
using LibraryManager.WebForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace LibraryManager.UnitTests
{
    [TestClass]
    public class ApiTests
    {
        private Mock<IBookService> _bookServiceMock;
        private BooksController _controller;

        [TestInitialize]
        public void Setup()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller = new BooksController(_bookServiceMock.Object);
        }

        [TestMethod]
        public async Task GetBooks_ReturnsAllBooks()
        {
            var expectedBooks = TestBooks.GetTestBooks().Select(book => new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Quantity = book.Quantity,
                PublicationYear = book.PublicationYear,
                CategoryId = book.CategoryId,
            }).AsQueryable();
            _bookServiceMock.Setup(service => service.GetBooksAsync()).ReturnsAsync(expectedBooks);

            var result = await _controller.GetBooksAsync();

            var contentResult = result as OkNegotiatedContentResult<IEnumerable<BookDto>>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(expectedBooks.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public async Task GetBookByIdAsync_ReturnsBook()
        {
            var bookId = 3; 
            var expectedBook = TestBooks.GetTestBooks().Where(book => book.BookId == bookId).Select(book => new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Quantity = book.Quantity,
                PublicationYear = book.PublicationYear,
                CategoryId = book.CategoryId
            }).First();
            _bookServiceMock.Setup(service => service.GetBookByIdAsync(bookId)).ReturnsAsync(expectedBook);

            var result = await _controller.GetBookAsync(bookId);

            var contentResult = result as OkNegotiatedContentResult<BookDto>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(expectedBook.BookId, contentResult.Content.BookId);
        }

        [TestMethod]
        public async Task CreateBookAsync_AddsBook()
        {
            var bookDto = new BookDto
            {
                BookId = 4,
                Title = "New Book",
                Author = "New Author",
                Quantity = 1,
                CategoryId = 1,
                ISBN = "111-111-1",
                PublicationYear = 2005
            };
            _bookServiceMock.Setup(service => service.CreateBookAsync(bookDto)).ReturnsAsync(bookDto);

            var result = await _controller.CreateBookAsync(bookDto);

            var createdResult = result as CreatedNegotiatedContentResult<BookDto>;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual($"books/{bookDto.BookId}", createdResult.Location.ToString());
        }

        [TestMethod]
        public async Task UpdateBookAsync_UpdatesBook()
        {
            var bookId = 1;
            var bookDto = new BookDto 
            { 
                BookId = bookId, 
                Title = "New Title",
            };
            _bookServiceMock.Setup(service => service.UpdateBookAsync(bookId, bookDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateBookAsync(bookId, bookDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public async Task UpdateBookWithInvalidValuesAsync_ReturnsBadRequest()
        {
            var bookId = 1;
            var bookDto = new BookDto
            {
                BookId = 2,
                Title = "New Title",
            };
            _bookServiceMock.Setup(service => service.UpdateBookAsync(bookId, bookDto)).ReturnsAsync(true);

            var result = await _controller.UpdateBookAsync(bookId, bookDto);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteBookAsync_DeletesBook()
        {
            var bookId = 2;
            _bookServiceMock.Setup(service => service.DeleteBookAsync(bookId)).ReturnsAsync(true);

            var result = await _controller.DeleteBookAsync(bookId);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, ((StatusCodeResult)result).StatusCode);
        }
    }
}
