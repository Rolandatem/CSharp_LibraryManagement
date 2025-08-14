using LibraryManagementApp.Interfaces;

namespace LibraryManagementApp.Services;

/// <summary>
/// The <c>ConsoleNotificationService</c> class provides a way to send notifications to the console.
/// </summary>
public class ConsoleNotificationService : INotificationService
{
    /// <summary>
    /// Sends a notification by writing a message to the console.
    /// </summary>
    /// <param name="message">The message to send as a notification.</param>
    public void Notify(string message)
    {
        Console.WriteLine($"Notification: {message}");
    }
}
