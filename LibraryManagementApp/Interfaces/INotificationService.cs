namespace LibraryManagementApp.Interfaces;

/// <summary>
/// Provides an abstraction for sending notification about library events.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Sends a notification.
    /// </summary>
    /// <param name="message">Message to use in notification.</param>
    void Notify(string message);
}
