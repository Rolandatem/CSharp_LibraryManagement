using LibraryManagementApp.Interfaces;
using LibraryManagementApp.Models;
using Moq;

namespace LibraryManagement.Tests.UnitTests;

/// <summary>
/// The <c>LibraryTests</c> class contains unit tests for managing a collection of books
/// within the Library Management System.
/// </summary>
public class LibraryTests
{
	#region "Tests"
	/// <summary>
	/// This test checks that the library does not allow adding duplicate books
	/// with the same title and author.
	/// </summary>
	[Fact]
	public void Test_AddBook_ShouldPreventDuplicateBooks()
	{
		//--Arrange: Create a library and add an initial book.
		Library library = new Library(new Mock<INotificationService>().Object);
		Book book = new Book("The Great Gatsby", "F. Scott Fitzgerald");
		library.AddBook(book);

		//--Act: Attempt to add the same book again.
		bool isAdded = library.AddBook(book);

		//--Assert: Verify that the duplicate book was not added.
		Assert.False(isAdded, "Duplicate book should not be added to the library.");
	}

	/// <summary>
	/// Tests that adding a book sends a notification.
	/// </summary>
	[Fact]
	public void Test_AddBook_ShouldSendNotification()
	{
        //--Arrange: Set up mock notification service and library.
        Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
		Library library = new Library(notificationServiceMock.Object);
		Book book = new Book("The Great Gatsby", "F. Scott Fitzgerald");

		//--Act: Add the book.
		library.AddBook(book);

		//--Assert: Verify notify method was called oncec with the correct message.
		notificationServiceMock.Verify(ns => ns.Notify("Book added: The Great Gatsby"), Times.Once);
	}

	/// <summary>
	/// This test ensures that a book can be removed from the library successfully.
	/// </summary>
	[Fact]
	public void Test_RemoveBook_ShouldRemoveBookSuccessfully()
	{
		//--Arrange: Create a library, add a book, and ensure it exists.
		Library library = new Library(new Mock<INotificationService>().Object);
		Book book = new Book("The Great Gatsby", "F. Scott Fitzgerald");
		library.AddBook(book);

		//--Act: Remove the book.
		bool isRemoved = library.RemoveBook(book);

		//--Assert: Verify that the book is removed.
		Assert.True(isRemoved, "The book should be removed from the library.");
		Assert.DoesNotContain(book, library.GetBooks());
	}

	/// <summary>
	/// Tests that removing a book sends a notification.
	/// </summary>
	[Fact]
	public void Test_RemoveBook_ShouldSendNotification()
	{
		//--Arrange: Set up mock notification service and a library with a book.
		Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
		Library library = new Library(notificationServiceMock.Object);
		Book book = new Book("The Great Gatsby", "F. Scott Fitzgerald");
		library.AddBook(book);

		//--Act: Remove the book.
		library.RemoveBook(book);

		//--Assert: Verify notify method was called once with the correct message.
		notificationServiceMock.Verify(ns => ns.Notify("Book removed: The Great Gatsby"), Times.Once);
	}

	/// <summary>
	/// This test checks the ability to search for a book by its title in the library.
	/// </summary>
	[Fact]
	public void Test_SearchBookByTitle_ShouldReturnCorrectBook()
	{
		//--Arrange: Create a library and add a few books.
		Library library = new Library(new Mock<INotificationService>().Object);
		Book book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald");
		Book book2 = new Book("1984", "George Orwell");
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
		Library library = new Library(new Mock<INotificationService>().Object);
		Book book = new Book("The Great Gatsby", "F. Scott Fitzgerald");
		library.AddBook(book);

		//--Act: Search for a book title that does not exist.
		Book? result = library.SearchBookByTitle("Invisible Man");

		//--Assert: Verify that the search returned null.
		Assert.Null(result);
	}

	/// <summary>
	/// Parameterized test case: Tests adding multiple books with varying titles.
	/// </summary>
	/// <param name="title">Title of the book.</param>
	/// <param name="author">Author of the book.</param>
	/// <param name="expected">Expected result of add operation (true if new, false if duplicate).</param>
	[Theory]
    [InlineData("The Great Gatsby", "F. Scott Fitzgerald", true)]
	[InlineData("1984", "George Orwell", true)]
    [InlineData("The Great Gatsby", "F. Scott Fitzgerald", false)]
	public void Test_AddBook_ShouldRespectDuplicates(
		string title,
		string author,
		bool expected)
	{
		//--Arrange: Create a library and add initial book if testing duplicate.
		Library library = new Library(new Mock<INotificationService>().Object);
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
