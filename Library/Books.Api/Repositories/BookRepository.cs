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

    public async Task<Book> AddNewBookAsync(BookDto book, long authorId, LibraryContext? libraryContext = null)
    {
        var context = libraryContext ?? await _libraryContextFactory.CreateDbContextAsync();

        var toAdd = new Book()
        {
            Title = book.Title,
            Price = book.Price,
            Bookstand = book.Bookstand,
            Shelf = book.Shelf,
            AuthorId = authorId,
        };

        await context.AddAsync(toAdd);
        await context.SaveChangesAsync();

        return toAdd;
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
