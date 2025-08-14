using System.Collections;

namespace LibraryManagement.Tests.DataGenerators;

/// <summary>
/// Demonstrates the use of a data generator for ClassData attribute when using Theory.
/// We can use this to not cloud a Theory test without clouding it up with InLineData attributes.
/// </summary>
public class BookDataGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "The Catcher in the Rye", "J.D. Salinger", true };
        yield return new object[] { "To Kill a Mockingbird", "Harper Lee", true };
        yield return new object[] { "The Great Gatsby", "F. Scrott Fitzgerald", false };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
