using LibraryManagement.Data.Models;

namespace LibraryManagement.Tests.UnitTests;

/// <summary>
/// The <c>BookTests</c> class contains unit tests for the Book entity in the Library Management System.
/// These tests ensure that the Book class functions as expected.
/// </summary>
public class BookTests
{
    #region "Tests"
    /// <summary>
    /// Test case: Verifies that a new book is correctly initialized with a given title.
    /// </summary>
    [Fact]
    public void Test_NewBook_ShouldHaveCorrectTitle()
    {
        //--Arrange: Set up a book title and create a new Book object.
        string bookTitle = "The Great Gatsby";
        Book book = new Book(bookTitle, "F. Scott Fitzgerald");

        //--Act: Retrieve the book's title.
        string result = book.Title;

        //--Assert: Verify that the book's title is as expected.
        Assert.Equal(bookTitle, result);
    }

    /// <summary>
    /// Test case: Verifies that a new book is correctly initialized with a given author.
    /// </summary>
    [Fact]
    public void Test_NewBook_ShouldHaveCorrectAuthor()
    {
        //--Arrange: Set up a book author and create a new Book object.
        string bookAuthor = "F. Scott Fitzgerald";
        Book book = new Book("The Great Gatsby", bookAuthor);

        //--Act: Retrieve the book's author.
        string result = book.Author;

        //--Assert: Verify that the book's author is as expected.
        Assert.Equal(bookAuthor, result);
    }
    #endregion
}
