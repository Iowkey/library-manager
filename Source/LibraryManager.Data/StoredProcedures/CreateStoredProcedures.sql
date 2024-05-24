CREATE PROCEDURE GetBooks
    @PageNumber INT,
    @PageSize INT,
    @SearchTerm NVARCHAR(100),
    @SortColumn NVARCHAR(50),
    @SortOrder NVARCHAR(4)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT;
    SET @Offset = (@PageNumber - 1) * @PageSize;

    DECLARE @SQL NVARCHAR(MAX);

    SET @SQL = N'SELECT * FROM (
                    SELECT 
                        BookId, 
                        Title, 
                        Author, 
                        ISBN, 
                        PublicationYear, 
                        Quantity, 
                        CategoryId, 
                        ROW_NUMBER() OVER (ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortOrder + ') AS RowNum
                    FROM Books
                    WHERE (@SearchTerm IS NULL 
                        OR Title LIKE ''%'' + @SearchTerm + ''%''
                        OR Author LIKE ''%'' + @SearchTerm + ''%''
                        OR ISBN LIKE ''%'' + @SearchTerm + ''%'')
                ) AS Paged
                WHERE RowNum > @Offset AND RowNum <= (@Offset + @PageSize)
                ORDER BY RowNum';

    EXEC sp_executesql 
        @SQL, 
        N'@PageSize INT, @Offset INT, @SearchTerm NVARCHAR(100)', 
        @PageSize, @Offset, @SearchTerm;
END;