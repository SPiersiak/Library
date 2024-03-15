using Books.Api.Models;

namespace Books.Api.Repositories.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetBooksAsync();
}
