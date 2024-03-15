using Books.Api.Models;

namespace Books.Api.Services.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetBooksAsync();
}
