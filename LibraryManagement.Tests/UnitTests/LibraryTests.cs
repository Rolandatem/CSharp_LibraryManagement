using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Tests.Fixtures;
using LibraryManagementApp.Interfaces;
using LibraryManagementApp.Services;
using Moq;

namespace LibraryManagement.Tests.UnitTests;

/// <summary>
/// The <c>LibraryTests</c> class contains unit tests for managing a collection of books
/// within the Library Management System.
/// </summary>
[Collection("LibraryManagementDbContext Collection")]
public class LibraryTests
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
    public LibraryTests(
		LibraryManagementDbContextFixture libraryFixture)
	{
		_libraryFixture = libraryFixture;
		_notificationServiceMock = new Mock<INotificationService>();
	}
	#endregion

	#region "Tests"
	/// <summary>
	/// Test case: Checks that the library does not allow adding duplicate books
	/// with the same title and author.
	/// </summary>
	[Fact]
	public void Test_AddBook_ShouldPreventDuplicateBooks()
	{
        //--Arrange: Create a library and add an initial book.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
		library.AddBook(book);

		//--Act: Attempt to add the same book again.
		bool isAdded = library.AddBook(book);

		//--Assert: Verify that the duplicate book was not added.
		Assert.False(isAdded, "Duplicate book should not be added to the library.");
	}

	/// <summary>
	/// Test case: Adding a book sends a notification.
	/// </summary>
	[Fact]
	public void Test_AddBook_ShouldSendNotification()
	{
        //--Arrange: Set up mock notification service and library.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
		LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);

		//--Act: Add the book.
		library.AddBook(book);

		//--Assert: Verify notify method was called once with the correct message.
		_notificationServiceMock.Verify(ns => ns.Notify($"Book added: {TheGreatGatsby}"), Times.Once);
	}

	/// <summary>
	/// Test case: Ensures that a book can be removed from the library successfully.
	/// </summary>
	[Fact]
	public void Test_RemoveBook_ShouldRemoveBookSuccessfully()
	{
        //--Arrange: Create a library, add a book, and ensure it exists.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
		library.AddBook(book);

		//--Act: Remove the book.
		bool isRemoved = library.RemoveBook(book);

		//--Assert: Verify that the book is removed.
		Assert.True(isRemoved, "The book should be removed from the library.");
		Assert.DoesNotContain(book, library.GetAllBooks());
	}

	/// <summary>
	/// Test case: Removing a book sends a notification.
	/// </summary>
	[Fact]
	public void Test_RemoveBook_ShouldSendNotification()
	{
        //--Arrange: Set up mock notification service and a library with a book.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
		library.AddBook(book);

		//--Act: Remove the book.
		library.RemoveBook(book);

		//--Assert: Verify notify method was called once with the correct message.
		_notificationServiceMock.Verify(ns => ns.Notify($"Book removed: {TheGreatGatsby}"), Times.Once);
	}

	/// <summary>
	/// Test case: Checks the ability to search for a book by its title in the library.
	/// </summary>
	[Fact]
	public void Test_SearchBookByTitle_ShouldReturnCorrectBook()
	{
        //--Arrange: Create a library and add a few books.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book1 = new Book(TheGreatGatsby, FScottFitzgerald);
		Book book2 = new Book("1984", GeorgeOrwell);
		library.AddBook(book1);
		library.AddBook(book2);

		//--Act: Search for a book by title.
		Book? result = library.SearchBookByTitle("1984");

		//--Assert: The correct book should be returned.
		Assert.NotNull(result);
		Assert.Equal(book2.Title, result.Title);
	}

	/// <summary>
	/// Test case: Ensures that searching for a non-existant book returns null.
	/// </summary>
	[Fact]
	public void Test_SearchBookByTitle_SearchNonExistantBook_ShouldReturnNull()
	{
        //--Arrange: Create a library and add a book.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book = new Book(TheGreatGatsby, FScottFitzgerald);
		library.AddBook(book);

		//--Act: Search for a book title that does not exist.
		Book? result = library.SearchBookByTitle("Invisible Man");

		//--Assert: Verify that the search returned null.
		Assert.Null(result);
	}

	/// <summary>
	/// Test case: Ensures all books are returned.
	/// </summary>
	[Fact]
	public void Test_GetAllBooks_ShouldReturnAllBooks()
	{
        //--Arrange: Create a library with books and a mock notification service.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        Book book1 = new Book(TheGreatGatsby, FScottFitzgerald);
		Book book2 = new Book("1984", GeorgeOrwell);
		library.AddBook(book1);
		library.AddBook(book2);

		//--Act: Retrieve books.
		IEnumerable<Book> allBooks = library.GetAllBooks();

		//--Assert: Verify the correct books are returned.
		Assert.Contains(book1, allBooks);
		Assert.Contains(book2, allBooks);
		Assert.Equal(2, allBooks.Count());
	}

	/// <summary>
	/// Test case: Correctly handles an empty library.
	/// </summary>
	[Fact]
	public void Test_GetAllBooks_ShouldReturnEmptyForEmptyLibrary()
	{
        //--Arrange: Create a library with no books and a mock notification service.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);

        //--Act: Retrieve books from an empty library.
        IEnumerable<Book> allBooks = library.GetAllBooks();

		//--Assert: Verify that the returned collection is empty.
		Assert.Empty(allBooks);
	}

	/// <summary>
	/// Parameterized test case: Tests adding multiple books with varying titles.
	/// </summary>
	/// <param name="title">Title of the book.</param>
	/// <param name="author">Author of the book.</param>
	/// <param name="expected">Expected result of add operation (true if new, false if duplicate).</param>
	[Theory]
    [InlineData(TheGreatGatsby, FScottFitzgerald, true)]
	[InlineData("1984", GeorgeOrwell, true)]
    [InlineData(TheGreatGatsby, FScottFitzgerald, false)]
	public void Test_AddBook_ShouldRespectDuplicates(
		string title,
		string author,
		bool expected)
	{
        //--Arrange: Create a library and add initial book if testing duplicate.
        using LibraryManagementDbContext context = new LibraryManagementDbContextFixture().Context;
        LibraryService library = new LibraryService(context, _notificationServiceMock.Object);
        if (expected == false)
		{
			//--Testing an expected response of 'false', so add initial book
			//--so 'Act' below is a duplicate.
			library.AddBook(new Book(title, author));
		}

		//--Act: Attempt to add book and check result.
		bool result = library.AddBook(new Book(title, author));

		//--Assert: Result should match expectation based on duplication.
		Assert.Equal(expected, result);
	}
    #endregion
}
