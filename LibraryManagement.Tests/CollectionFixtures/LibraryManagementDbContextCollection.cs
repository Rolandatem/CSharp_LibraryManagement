using LibraryManagement.Tests.Fixtures;

namespace LibraryManagement.Tests.CollectionFixtures;

/// <summary>
/// Fixture collection that allows the use of the <see cref="LibraryManagementDbContextFixture"/> in
/// multiple tests.
/// </summary>
[CollectionDefinition("LibraryManagementDbContext Collection")]
public class LibraryManagementDbContextCollection
    : ICollectionFixture<LibraryManagementDbContextFixture>
{
}
