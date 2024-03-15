using Books.Api.Models;
using Books.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbContextFactory<LibraryContext> _libraryContextFactory;

    public BookRepository(IDbContextFactory<LibraryContext> libraryContextFactory)
    {
        _libraryContextFactory = libraryContextFactory;
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        using var context = await _libraryContextFactory.CreateDbContextAsync();

        var query = await context.Books
            .Include(i => i.Author)
            .AsNoTracking()
            .ToListAsync();

        return query;
    }
}
