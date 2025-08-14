using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Tests.Fixtures;

/// <summary>
/// Database configuration fixture that allows us to use the in-memory database
/// for testing instead of the real database.
/// </summary>
public class LibraryManagementDbContextFixture : IDisposable
{
	#region "Member Variables"
	private readonly DbContextOptions<LibraryManagementDbContext> _options;
	#endregion

	#region "Public Properties"
	public LibraryManagementDbContext Context { get; private set; }
	#endregion

	#region "Constructor"
	public LibraryManagementDbContextFixture()
	{
		//--Create unique database names for isolation on every test run.
		_options = new DbContextOptionsBuilder<LibraryManagementDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		this.Context = new LibraryManagementDbContext(_options);

		//--Initialize data if required.
		SeedDatabase();
	}
	#endregion

	#region "Private Methods"
	private void SeedDatabase()
	{
		//--Nothing yet.
	}
	#endregion

	#region "IDisposable"
	public void Dispose()
	{
		this.Context.Dispose();
	}
	#endregion
}
