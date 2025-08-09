using LibraryManagementApp.Interfaces;

namespace LibraryManagementApp.Models;

/// <summary>
/// The <c>Library</c> class represents a collection of books in the Library Management System.
/// </summary>
public class Library
{
	#region "Member Variables"
	private readonly List<Book> _books;
	private readonly INotificationService _notificationService;
    #endregion

    #region "Constructor"
    /// <summary>
    /// Initializes a new instance of the <see cref="Library"/> class.
    /// </summary>
    /// <param name="notificationService">An instance of <see cref="INotificationService"/> used to send
	/// notifications when books ar added or removed.</param>
    public Library(INotificationService notificationService)
	{
		_books = new List<Book>();
		_notificationService = notificationService;
	}
	#endregion

	#region "Public Methods"
	/// <summary>
	/// Attempts to add a book to the library.
	/// </summary>
	/// <param name="newBook">The book to add.</param>
	/// <returns>True if the book was added successfully, otherwise false if the book already exists.</returns>
	public bool AddBook(Book newBook)
	{
		if (_books.Exists(book => 
			book.Title == newBook.Title &&
			book.Author == newBook.Author))
		{
			//--Book already exists, do not add.
			return false;
		}

		_books.Add(newBook);
		_notificationService.Notify($"Book added: {newBook.Title}");
		return true;
	}

	/// <summary>
	/// Removes a book from the library.
	/// </summary>
	/// <param name="book">The book to remove.</param>
	/// <returns>True if the book was removed, otherwise false if the book was not found.</returns>
	public bool RemoveBook(Book book)
	{
		bool removed = _books.Remove(book);
		if (removed)
		{
			_notificationService.Notify($"Book removed: {book.Title}");
		}

		return removed;
	}

	/// <summary>
	/// Returns a list of all books in the library.
	/// </summary>
	/// <returns></returns>
	public IEnumerable<Book> GetBooks()
	{
		return _books;
	}

	/// <summary>
	/// Searches for a book by it's title.
	/// </summary>
	/// <param name="title">The title of the book to search for.</param>
	/// <returns>The book if found, otherwise null.</returns>
	public Book? SearchBookByTitle(string title)
	{
		return _books
			.FirstOrDefault(book => book.Title.ToLower() == title.ToLower());
	}

	/// <summary>
	/// Asynchromously retrieves all books in the library.
	/// </summary>
	/// <returns>A task representing the retrieval of a list of books.</returns>
	public async Task<IEnumerable<Book>> GetAllBooksAsync()
	{
		//--Simulate async operation (e.g, fetching from a database).
		await Task.Delay(1000);
		return _books;
	}

	/// <summary>
	/// Asynchronously searched for a book by its title.
	/// </summary>
	/// <param name="title">The title of the book to search for.</param>
	/// <returns>A task that returns a book if found, otherwise null.</returns>
	public async Task<Book?> SearchBookByTitleAsync(string title)
	{
		//--Simulate async operation.
		await Task.Delay(1000);
		return _books.FirstOrDefault(book => book.Title.ToLower() == title.ToLower());
	}
	#endregion
}
