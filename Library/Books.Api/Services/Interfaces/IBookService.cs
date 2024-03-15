using Books.Api.Models;
using Commons.ResultHelper;

namespace Books.Api.Services.Interfaces;

public interface IBookService
{
    Task<ResultHelper<BookDto>> AddNewBookAsync(BookDto book);

    Task<List<BookDto>> GetBooksAsync();
}
