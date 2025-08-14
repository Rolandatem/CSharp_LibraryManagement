using System.Diagnostics.CodeAnalysis;

namespace LibraryManagement.Data.Models;

/// <summary>
/// The <c>Book</c> class represents a book in the Library Management System.
/// </summary>
[ExcludeFromCodeCoverage]
public class Book
{
	#region "Constructors"
	/// <summary>
	/// Initializes an empty instance of the <see cref="Book"/> class.
	/// </summary>
	public Book() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="Book"/> class.
	/// </summary>
	/// <param name="title">The title of the book.</param>
	/// <param name="author">The author of the book.</param>
	public Book(
		string title,
		string author)
	{
		this.Title = title;
		this.Author = author;
	}
	#endregion

	#region "Public Properties"
	/// <summary>
	/// DB ID of the book.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the title of the book.
	/// </summary>
	public string Title { get; set; } = String.Empty;

	/// <summary>
	/// Gets or sets the author of the book.
	/// </summary>
	public string Author { get; set; } = String.Empty;
	#endregion
}
