using LibraryManagement.Data.Models;

namespace LibraryManagementApp.Interfaces;

/// <summary>
/// Abstraction of the library service.
/// </summary>
public interface ILibraryService
{
    #region "Sync Methods"
    /// <summary>
    /// Attempts to add a book to the library.
    /// </summary>
    /// <param name="newBook">The book to add.</param>
    /// <returns>True if the book was added successfully, otherwise false if the book already exists.</returns>
    bool AddBook(Book newBook);

    /// <summary>
    /// Removes a book from the library.
    /// </summary>
    /// <param name="book">The book to remove.</param>
    /// <returns>True if the book was removed, otherwise false if the book was not found.</returns>
    bool RemoveBook(Book book);

    /// <summary>
	/// Returns a list of all books in the library.
	/// </summary>
	/// <returns>A collection of existing books in the repository.</returns>
	IEnumerable<Book> GetAllBooks();

    /// <summary>
	/// Searches for a book by it's title.
	/// </summary>
	/// <param name="title">The title of the book to search for.</param>
	/// <returns>The book if found, otherwise null.</returns>
	Book? SearchBookByTitle(string title);
    #endregion

    #region "Async Methods"
    /// <summary>
    /// Asynchronously attempts to add a book to the library.
    /// </summary>
    /// <param name="newBook">The book to add.</param>
    /// <returns>True if the book was added successfully, otherwise false if the book already exists.</returns>
    Task<bool> AddBookAsync(Book newBook);

    /// <summary>
	/// Asynchronously removes a book from the library.
	/// </summary>
	/// <param name="book">The book to remove.</param>
	/// <returns>True if the book was removed, otherwise false if the book was not found.</returns>
	Task<bool> RemoveBookAsync(Book book);

    /// <summary>
    /// Asynchromously retrieves all books in the library.
    /// </summary>
    /// <returns>A collection of existing books in the repository.</returns>
	Task<IEnumerable<Book>> GetAllBooksAsync();

    /// <summary>
    /// Asynchronously searches for a book by its title.
    /// </summary>
    /// <param name="title">The title of the book to search for.</param>
    /// <returns>The book if found, otherwise null.</returns>
	Task<Book?> SearchBookByTitleAsync(string title);
	#endregion
}
