using Moq;
using System;
using System.Data.Entity;

namespace LibraryManager.UnitTests.Extensions
{
    public static class DbSetMockExtensions
    {
        public static void SetupFindAsync<T>(this Mock<DbSet<T>> mockSet, Func<object[], T> find) where T : class
        {
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) => find(ids));
        }
    }
}
