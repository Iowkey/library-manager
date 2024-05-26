using LibraryManager.Data.DataContext;
using LibraryManager.Data.Entities;
using LibraryManager.Data.Helpers;
using LibraryManager.Data.Repositories;
using LibraryManager.UnitTests.Extensions;
using LibraryManager.UnitTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.UnitTests
{
    [TestClass]
    public class DataAccessLayerTests
    {
        private Mock<LibraryContext> _contextMock;
        private Mock<IDatabaseOperations> _databaseOperationsMock;
        private Mock<DbSet<Book>> _booksMock;
        private BookRepository _bookRepository;

        [TestInitialize]
        public void Setup()
        {
            var testBooks = TestBooks.GetTestBooks().AsQueryable();

            _booksMock = new Mock<DbSet<Book>>();
            _booksMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(testBooks.Provider);
            _booksMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(testBooks.Expression);
            _booksMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(testBooks.ElementType);
            _booksMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(testBooks.GetEnumerator());
            _booksMock.SetupFindAsync(ids => testBooks.FirstOrDefault(b => b.BookId == (int)ids[0]));


            _contextMock = new Mock<LibraryContext>();
            _contextMock.Setup(c => c.Books).Returns(_booksMock.Object);

            _databaseOperationsMock = new Mock<IDatabaseOperations>();
            _databaseOperationsMock
                .Setup(d => d.GetBooksAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(testBooks.ToList());

            _bookRepository = new BookRepository(_contextMock.Object, _databaseOperationsMock.Object);
        }

        [TestMethod]
        public async Task GetBooks_ReturnsAllBooks()
        {
            var books = await _bookRepository.GetBooksAsync();

            Assert.IsNotNull(books);
            Assert.AreEqual(3, books.Count());
        }

        [TestMethod]
        public async Task GetBookById_ReturnsCorrectBook()
        {
            var bookId = 2;
            var expectedBook = _contextMock.Object.Books.First(b => b.BookId == bookId);

            var book = await _bookRepository.GetBookByIdAsync(bookId);

            Assert.IsNotNull(book);
            Assert.AreEqual(expectedBook.BookId, book.BookId);
            Assert.AreEqual(expectedBook.Title, book.Title);

        }

        [TestMethod]
        public async Task AddBook_AddsBookToDatabase()
        {
            var book = new Book { Title = "New Book", Author = "New Author", Quantity = 10, PublicationYear = 2021 };

            await _bookRepository.AddBookAsync(book);
            // Assert
            _booksMock.Verify(m => m.Add(It.IsAny<Book>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddBookWithInvalidQuantity_ThrowsAnException()
        {
            var book = new Book { Title = "New Book", Author = "New Author", Quantity = -4, PublicationYear = 2021 };

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _bookRepository.AddBookAsync(book));
        }

        [TestMethod]
        public async Task AddBookWithInvalidPublicationYear_ThrowsAnException()
        {
            var book = new Book { Title = "New Book", Author = "New Author", Quantity = 5, PublicationYear = -1000 };

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _bookRepository.AddBookAsync(book));
        }

        [TestMethod]
        public async Task UpdateBook_UpdatesBookInDatabase()
        {
            // Arrange
            var bookToUpdate = _contextMock.Object.Books.First();
            bookToUpdate.Title = "Updated Title";

            await _bookRepository.UpdateBookAsync(bookToUpdate);

            _contextMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task DeleteBook_DeletesBookFromDatabase()
        {
            var bookToDelete = _contextMock.Object.Books.First();

            await _bookRepository.DeleteBookAsync(bookToDelete.BookId);

            _booksMock.Verify(m => m.Remove(It.IsAny<Book>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(), Times.Once);
        }
    }
}
