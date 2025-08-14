using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagement.Data.Configurations;

[ExcludeFromCodeCoverage]
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Author)
            .HasMaxLength(100)
            .IsRequired();
    }
}