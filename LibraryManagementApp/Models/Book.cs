namespace LibraryManagementApp.Models;

/// <summary>
/// The <c>Book</c> class represents a book in the Library Management System.
/// </summary>
public class Book
{
	#region "Constructor"
	/// <summary>
	/// Initializes a new instance of the <see cref="Book"/> class.
	/// </summary>
	/// <param name="title">The title of the book.</param>
	/// <param name="author">The author of the book.</param>
	public Book(string title, string author)
	{
		this.Title = title;
		this.Author = author;
	}
	#endregion

	#region "Public Properties"
	/// <summary>
	/// Gets the title of the book.
	/// </summary>
	public string Title { get; }

	/// <summary>
	/// Gets the author of the book.
	/// </summary>
	public string Author { get; }
	#endregion
}
