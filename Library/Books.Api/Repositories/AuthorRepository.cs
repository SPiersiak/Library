using Books.Api.Models;
using Books.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly IDbContextFactory<LibraryContext> _libraryContext;

    public AuthorRepository(IDbContextFactory<LibraryContext> libraryContext)
    {
        _libraryContext = libraryContext;
    }

    public async Task<Author> AddAuthorAsync(AuthorDto author, LibraryContext? libraryContext = null)
    {
        var context = libraryContext ?? await _libraryContext.CreateDbContextAsync();

        var toAdd = new Author()
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
        };

        await context.AddAsync(toAdd);
        await context.SaveChangesAsync();

        return toAdd;
    }

    public async Task<(bool, long)> CheckIfAuthorExistAsync(AuthorDto author, LibraryContext? libraryContext = null)
    {
        var context = libraryContext ?? await _libraryContext.CreateDbContextAsync();

        long query =  await context.Authors
            .Where(x => x.FirstName.ToLower().Equals(author.FirstName.ToLower()) && x.LastName.ToLower().Equals(author.LastName.ToLower()))
            .Select(s => s.Id)
            .FirstOrDefaultAsync();

        return (query != default(long), query);
    }
}
