using Books.Api.Models;

namespace Books.Api.Repositories.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetBooksAsync();

    Task<Book> AddNewBookAsync(BookDto book, long authorId, LibraryContext? libraryContext = null);
}
