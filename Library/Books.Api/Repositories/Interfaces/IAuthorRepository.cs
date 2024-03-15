using Books.Api.Models;

namespace Books.Api.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<(bool, long)> CheckIfAuthorExistAsync(AuthorDto author, LibraryContext? libraryContext = null);

    Task<Author> AddAuthorAsync(AuthorDto author, LibraryContext? libraryContext = null);
}
