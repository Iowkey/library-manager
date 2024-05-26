using LibraryManager.Data.DataContext;
using LibraryManager.Data.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace LibraryManager.Data.Helpers
{
    public interface IDatabaseOperations
    {
        Task<List<Book>> GetBooksAsync(int pageNumber, int pageSize, string searchTerm, string sortColumn, string sortOrder);
    }

    public class DatabaseOperations : IDatabaseOperations
    {
        private readonly LibraryContext _context;

        public DatabaseOperations(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync(int pageNumber, int pageSize, string searchTerm, string sortColumn, string sortOrder)
        {
            return await _context.Database.SqlQuery<Book>(
                "EXEC GetBooks @PageNumber, @PageSize, @SearchTerm, @SortColumn, @SortOrder",
                new SqlParameter("@PageNumber", 1),
                new SqlParameter("@PageSize", int.MaxValue),
                new SqlParameter("@SearchTerm", (object)searchTerm ?? DBNull.Value),
                new SqlParameter("@SortColumn", "BookId"),
                new SqlParameter("@SortOrder", "ASC")
            ).ToListAsync();

        }
    }
}
