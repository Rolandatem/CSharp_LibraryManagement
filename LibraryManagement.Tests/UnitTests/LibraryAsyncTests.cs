using LibraryManagementApp.Interfaces;
using LibraryManagementApp.Models;
using Moq;

namespace LibraryManagement.Tests.UnitTests;

/// <summary>
/// Tests for asynchronous methods in the Library class.
/// </summary>
public class LibraryAsyncTests
{
    /// <summary>
    /// Tests that <see cref="Library.GetAllBooksAsync"/> correctly retrieves books asynchronously.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_GetAllBooksAsync_ShouldReturnAllBooks()
    {
        //--Arrange: Create a library with books and a mock notification service.
        Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
        Library library = new Library(notificationServiceMock.Object);
        Book book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald");
        Book book2 = new Book("1984", "George Orwell");
        library.AddBook(book1);
        library.AddBook(book2);

        //--Act: Retrieve books asynchronously.
        IEnumerable<Book> allBooks = await library.GetAllBooksAsync();

        //--Assert: Verify the correct books are returned.
        Assert.Contains(book1, allBooks);
        Assert.Contains(book2, allBooks);
        Assert.Equal(2, allBooks.Count());
    }

    /// <summary>
    /// Tests that <see cref="Library.GetAllBooksAsync"/> correctly handles an empty library.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_GetAllBooksAsync_ShouldReturnEmptyForEmptyLibrary()
    {
        //--Arrange: Create a library with no books and a mock notification service.
        Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
        Library library = new Library(notificationServiceMock.Object);

        //--Act: Retrieve books asynchronously from an empty library.
        IEnumerable<Book> allBooks = await library.GetAllBooksAsync();

        //--Assert: Verify that the returned collection is empty.
        Assert.Empty(allBooks);
    }

    /// <summary>
    /// Tests that <see cref="Library.SearchBookByTitleAsync(string)"/> correctly finds a book by title asynchronously.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_SearchBookByTitleAsync_ShouldReturnCorrectBook()
    {
        //--Arrange: Create a library and add books wth a mock notification service.
        Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
        Library library = new Library(notificationServiceMock.Object);
        Book book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald");
        Book book2 = new Book("1984", "George Orwell");
        library.AddBook(book1);
        library.AddBook(book2);

        //--Act: Search for a book asynchronously
        Book? foundBook = await library.SearchBookByTitleAsync("1984");

        //--Assert: Verify that the correct book is returned.
        Assert.NotNull(foundBook);
        Assert.Equal(book2.Title, foundBook.Title);
    }
}
