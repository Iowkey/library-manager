using LibraryManager.Data.Entities;
using System.Collections.Generic;

namespace LibraryManager.UnitTests.TestData
{
    public static class TestBooks
    {
        public static List<Book> GetTestBooks()
        {
            return new List<Book>
            {
                new Book { BookId = 1, Title = "Book1", Author = "Author1", ISBN = "123-4-56", PublicationYear = 1967, Quantity = 5, CategoryId = 1 },
                new Book { BookId = 2, Title = "Book2", Author = "Author2", ISBN = "123-4-57", PublicationYear = -5, Quantity = 5, CategoryId = 2 },
                new Book { BookId = 3, Title = "Book3", Author = "Author3", ISBN = "123-4-26", PublicationYear = 1990, Quantity = -100, CategoryId = 1 },
            };
        }
    }
}
