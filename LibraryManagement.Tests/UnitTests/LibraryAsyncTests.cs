using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Tests.DataGenerators;
using LibraryManagement.Tests.Fixtures;
using LibraryManagementApp.Interfaces;
using LibraryManagementApp.Services;
using Moq;

namespace LibraryManagement.Tests.UnitTests;

/// <summary>
/// Tests for asynchronous methods in the Library class.
/// </summary>
[Collection("LibraryManagementDbContext Collection")]
public class LibraryAsyncTests
{
    #region "Member Variables"
    private readonly LibraryManagementDbContextFixture _libraryFixture;
    private readonly Mock<INotificationService> _notificationServiceMock;

    //--A couple of values for reference just to make sure spelling is always correct and intellisense help.
    public const string TheGreatGatsby = "The Great Gatsby";
    public const string FScottFitzgerald = "F. Scott Fitzgerald";
    public const string GeorgeOrwell = "George Orwell";
    #endregion

    #region "Constructor"
    public LibraryAsyncTests(
        LibraryManagementDbContextFixture libraryFixture)
    {
        _libraryFixture = libraryFixture;
        _notificationServiceMock = new Mock<INotificationService>();
    }
    #endregion

    #region "Tests"
    /// <summary>
    /// Test case: Checks that the library does not allow adding duplicate books with 
    /// the same title and author.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_AddBookAsync_ShouldPreventDuplicateBooks()
    {
        //--Arrange: Create a library and add an initial book.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
        await library.AddBookAsync(book);

        //--Act: Attempt to add the same book again.
        bool isAdded = await library.AddBookAsync(book);

        //--Assert: Verify that the duplicate book was not added.
        Assert.False(isAdded, "Duplicate book should not be added to the library");
    }

    /// <summary>
    /// Test case: Adding a book asynchronously sends a notification.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_AddBookAsync_ShouldSendNotification()
    {
        //--Arrange: Set up mock notification service and library.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);

        //--Act: Add the book.
        await library.AddBookAsync(book);

        //--Assert: Verify notify method was called once with the correct method.
        _notificationServiceMock.Verify(ns => ns.Notify($"Book added: {TheGreatGatsby}"), Times.Once);
    }

    /// <summary>
    /// Test case: Ensures that a book can be removed asynchronously from the library successfully.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_RemoveBookAsync_ShouldRemoveBookSuccessfully()
    {
        //--Arrange: Create a library, add a book, and ensure it exists.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
        await library.AddBookAsync(book);

        //--Act: Remove the book.
        bool isRemoved = await library.RemoveBookAsync(book);

        //--Assert: Verify that the book is removed.
        Assert.True(isRemoved, "The book should be removed from the library.");
        Assert.DoesNotContain(book, await library.GetAllBooksAsync());
    }

    /// <summary>
    /// Test case: Removing a book asynchronously sends a notification.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_RemoveBookAsync_ShouldSendNotification()
    {
        //--Arrange: Set up mock notification service and library with a book.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
        await library.AddBookAsync(book);

        //--Act: Remove the book
        await library.RemoveBookAsync(book);

        //--Assert:
        _notificationServiceMock.Verify(ns => ns.Notify($"Book added: {TheGreatGatsby}"), Times.Once);
    }

    /// <summary>
    /// Test case: Checks the ability to search for a book by title asynchronously.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_SearchBookByTitleAsync_ShouldReturnCorrectBook()
    {
        //--Arrange: Create a library and add a few books.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book1 = new Book(TheGreatGatsby, FScottFitzgerald);
        Book book2 = new Book("1984", GeorgeOrwell);
        await library.AddBookAsync(book1);
        await library.AddBookAsync(book2);

        //--Act: search for a book by title.
        Book? result = await library.SearchBookByTitleAsync("1984");

        //--Assert: The correct book should be returned.
        Assert.NotNull(result);
        Assert.Equal(book2.Title, result.Title);
    }

    /// <summary>
    /// Test case: Ensures that searching for a non-existant book asynchronously returns null.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_SearchBookByTitleAsync_SearchNonExistantBook_ShouldReturnNull()
    {
        //--Arrange: Create a library and add a book.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
        await library.AddBookAsync(book);

        //--Act: Search for a book title that does not exist.
        Book? result = await library.SearchBookByTitleAsync("Invisible Man");

        //--Assert: Verify that the search returned null.
        Assert.Null(result);
    }
 
    /// <summary>
    /// Test case: Ensures all books are returned asynchronously.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_GetAllBooksAsync_ShouldReturnAllBooks()
    {
        //--Arrange: Create a library with books and a mock notification service.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book1 = new Book(TheGreatGatsby, FScottFitzgerald);
        Book book2 = new Book("1984", GeorgeOrwell);
        await library.AddBookAsync(book1);
        await library.AddBookAsync(book2);

        //--Act: Retrieve books asynchronously.
        IEnumerable<Book> allBooks = await library.GetAllBooksAsync();

        //--Assert: Verify the correct books are returned.
        Assert.Contains(book1, allBooks);
        Assert.Contains(book2, allBooks);
        Assert.Equal(2, allBooks.Count());
    }

    /// <summary>
    /// Test case: Correctly handles an empty library.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_GetAllBooksAsync_ShouldReturnEmptyForEmptyLibrary()
    {
        //--Arrange: Create a library with no books and a mock notification service.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);

        //--Act: Retrieve books asynchronously from an empty library.
        IEnumerable<Book> allBooks = await library.GetAllBooksAsync();

        //--Assert: Verify that the returned collection is empty.
        Assert.Empty(allBooks);
    }

    /// <summary>
    /// Parameterized test case: Tests adding multiple books asynchronously with varying titles and
    /// checking for duplicates.
    /// </summary>
    /// <param name="title">Title of the book.</param>
    /// <param name="author">Author of the book.</param>
    /// <param name="expected">Expected result of add operation (true if new, false if duplicate).</param>
    /// <returns></returns>
    [Theory]
    [InlineData(TheGreatGatsby, FScottFitzgerald, true)]
    [InlineData("1984", GeorgeOrwell, true)]
    [InlineData(TheGreatGatsby, FScottFitzgerald, false)]
    [ClassData(typeof(BookDataGenerator))]
    public async Task Test_AddBookAsync_ShouldRespectDuplicates(
        string title,
        string author,
        bool expected)
    {
        //--Arrange: Create a library and add initial book if testing duplicate.
        LibraryService library = new LibraryService(_libraryFixture.Context, _notificationServiceMock.Object);
        if (expected == false)
        {
            //--Testing an expected response of 'false', so add initial book
            //--so 'Act' below is a duplicate.
            await library.AddBookAsync(new Book(title, author));
        }

        //--Act: Attempt to add book and check result.
        bool result = await library.AddBookAsync(new Book(title, author));

        //--Assert: Result should match expectation based on duplication.
        Assert.Equal(expected, result);
    }
    #endregion
}
