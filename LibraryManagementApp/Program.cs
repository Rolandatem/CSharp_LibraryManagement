using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagementApp.Interfaces;
using LibraryManagementApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

#region "Application Initialization"
//--Configure DI
ServiceProvider serviceProvider = new ServiceCollection()
    //--In a non-test environment set up using an actual database service, but for this unit testing example we are just going
    //--to use the in-memory database as well.
    .AddDbContext<LibraryManagementDbContext>(options => options.UseInMemoryDatabase("LibraryDb"), ServiceLifetime.Transient)

    //--Services.
    .AddSingleton<INotificationService, ConsoleNotificationService>()
    .AddSingleton<ILibraryService, LibraryService>()

    //--Build provider.
    .BuildServiceProvider();

//--Resolve the library from the service provider.
ILibraryService libraryService = serviceProvider.GetRequiredService<ILibraryService>();

Console.Clear();
Console.WriteLine("Library Management System Initialized.");

await MainMenuAsync();
#endregion

[ExcludeFromCodeCoverage]
async Task MainMenuAsync()
{
    bool optionError = false;

    while (true)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();

        if (optionError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid option.");
            optionError = false;
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Select an option:");
        Console.WriteLine("1. Add a Book");
        Console.WriteLine("2. Remove a Book");
        Console.WriteLine("3. List All Books");
        Console.WriteLine("4. Search Book by Title");
        Console.WriteLine("5. Exit");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine();
        Console.Write("CHOICE: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await DoAddBookAsync();
                break;

            case "2":
                await DoRemoveBookAsync();
                break;

            case "3":
                await DoListAllBooksAsync();
                break;

            case "4":
                await DoSearchBookByTitleAsync();
                break;

            case "5":
                return;

            default:
                optionError = true;
                break;
        }
    }
}

[ExcludeFromCodeCoverage]
async Task DoAddBookAsync()
{
    string? title = null;
    string? author = null;

    Console.Clear();
    Console.WriteLine();
    Console.WriteLine();

    while (title == null || title == String.Empty)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Book Title: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        title = Console.ReadLine();

        if (title == null || title == String.Empty)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Title is required.");
        }
    }

    Console.WriteLine();
    while (author == null || author == String.Empty)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Author: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        author = Console.ReadLine();

        if (author == null || author == String.Empty)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Author is required.");
        }
    }

    Console.WriteLine();
    Book book = new Book(title, author);
    if (await libraryService.AddBookAsync(book)) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Book Added Successfully!");
    } 
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("A book with this title and author already exists!");
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Press any key to continue...");
    Console.ReadLine();
}

[ExcludeFromCodeCoverage]
async Task DoRemoveBookAsync()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("ID of Book to remove: ");
    Console.ForegroundColor = ConsoleColor.Gray;
    if (int.TryParse(Console.ReadLine(), out int bookId))
    {
        Book book = new Book() { Id = bookId };
        if (await libraryService.RemoveBookAsync(book))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book Removed Successfully!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Book not found.");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid Book ID.");
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Press any key to continue...");
    Console.ReadLine();
}

[ExcludeFromCodeCoverage]
async Task DoListAllBooksAsync()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.White;
    IEnumerable<Book> books = await libraryService.GetAllBooksAsync();

    if (books.Any() == false)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("There are currently no books in the library.");
    }
    else
    {
        Console.WriteLine("Books in the library:");

        foreach (Book book in books)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("ID: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(book.Id);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" Title: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(book.Title);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" Author: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(book.Author);
        }
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Press any key to continue.");
    Console.ReadLine();
}

[ExcludeFromCodeCoverage]
async Task DoSearchBookByTitleAsync()
{
    string? title = null;

    Console.Clear();
    Console.WriteLine();
    Console.WriteLine();

    while (title == null)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Enter the title of the book to search: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        title = Console.ReadLine();

        if (title == null || title == String.Empty)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Title is required.");
            Console.WriteLine();
        }
    }

    Book? book = await libraryService.SearchBookByTitleAsync(title);
    if (book == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No matching book found.");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("ID: ");
        Console.ForegroundColor= ConsoleColor.Cyan;
        Console.Write(book.Id);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(" Title: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(book.Title);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(" Author: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(book.Author);
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Press any key to continue.");
    Console.ReadLine();
}