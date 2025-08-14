using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Services;

/// <summary>
/// The <c>Library</c> class represents a collection of books in the
/// Library Management System.
/// </summary>
public class LibraryService : ILibraryService
{
	#region "Member Variables"
	private readonly LibraryManagementDbContext _context;
	private readonly INotificationService _notificationService;
    #endregion

    #region "Constructor"
    /// <summary>
    /// Initializes a new instance of the <see cref="LibraryService"/> class.
    /// </summary>
    /// <param name="context">An instance of the <c>LibraryManagementDbContext</c>.</param>
    /// <param name="notificationService">An instance of <see cref="INotificationService"/> used 
	/// to send notifications when books ar added or removed.</param>
    public LibraryService(
		LibraryManagementDbContext context,
		INotificationService notificationService)
	{
		_context = context;
		_notificationService = notificationService;
	}
    #endregion

    #region "ILibraryService"

    #region "Sync Public Methods"
    public bool AddBook(Book newBook)
    {
        if (_context.Books.Any(book =>
            book.Title == newBook.Title &&
            book.Author == newBook.Author))
        {
            //--Book already exists, do not add.
            return false;
        }

        _context.Books.Add(newBook);
        _context.SaveChanges();
        _notificationService.Notify($"Book added: {newBook.Title}");
        return true;
    }

    public bool RemoveBook(Book book)
    {
        Book? existingBook = _context.Books.Find(book.Id);
        if (existingBook == null)
        {
            //--Book does not exist.
            return false;
        }

        _context.Books.Remove(existingBook);
        _context.SaveChanges();
        _notificationService.Notify($"Book removed: {book.Title}");
        return true;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _context.Books
            .ToList();
    }

    public Book? SearchBookByTitle(string title)
    {
        return _context.Books
            .FirstOrDefault(book => book.Title.ToLower() == title.ToLower());
    }
    #endregion

    #region "Async Public Methods"
    public async Task<bool> AddBookAsync(Book newBook)
    {
        if (await _context.Books.AnyAsync(book =>
            book.Title == newBook.Title &&
            book.Author == newBook.Author))
        {
            //--Book already exists, do not add.
            return false;
        }

        await _context.Books.AddAsync(newBook);
        await _context.SaveChangesAsync();
        _notificationService.Notify($"Book added: {newBook.Title}");
        return true;
    }

    public async Task<bool> RemoveBookAsync(Book book)
    {
        Book? existingBook = await _context.Books.FindAsync(book.Id);
        if (existingBook == null)
        {
            //--Book does not exist.
            return false;
        }

        _context.Books.Remove(existingBook);
        await _context.SaveChangesAsync();
        _notificationService.Notify($"Book removed: {existingBook.Title}");
        return true;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books
            .ToListAsync();
    }

    public async Task<Book?> SearchBookByTitleAsync(string title)
    {
        return await _context.Books
            .FirstOrDefaultAsync(book => book.Title.ToLower() == title.ToLower());
    }
    #endregion

    #endregion
}
