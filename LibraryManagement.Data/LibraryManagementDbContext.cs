using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LibraryManagement.Data;

[ExcludeFromCodeCoverage]
public class LibraryManagementDbContext : DbContext
{
	#region "Tables"
	public DbSet<Book> Books { get; set; }
    #endregion

    #region "Constructor"
    public LibraryManagementDbContext(
        DbContextOptions<LibraryManagementDbContext> options)
        : base(options)
    {

    }
    #endregion

    #region "Overrides"
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Assembly assembly = GetType().Assembly; 
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
    #endregion
}
