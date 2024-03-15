using Books.Api.Models;
using Books.Api.Repositories.Interfaces;
using Books.Api.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> GetBooksAsync()
    {
        var result = await _bookRepository.GetBooksAsync();
        return result.Adapt<List<BookDto>>();
    }
}
