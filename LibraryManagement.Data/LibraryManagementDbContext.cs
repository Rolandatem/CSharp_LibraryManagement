using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryManagement.Data;

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
