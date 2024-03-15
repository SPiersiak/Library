using Books.Api.Models;
using Books.Api.Repositories.Interfaces;
using Books.Api.Services.Interfaces;
using Commons.ResultHelper;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IDbContextFactory<LibraryContext> _libraryContext;

    public BookService(IBookRepository bookRepository, IDbContextFactory<LibraryContext> libraryContext, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _libraryContext = libraryContext;
        _authorRepository = authorRepository;
    }

    public async Task<ResultHelper<BookDto>> AddNewBookAsync(BookDto book)
    {
        using var context = await _libraryContext.CreateDbContextAsync();
        context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        context.Database.BeginTransaction();

        try
        {
            var (isExist, authorId) = await _authorRepository.CheckIfAuthorExistAsync(book.Author, context);

            if (!isExist && authorId == default(long))
            {
                var newAuthor = await _authorRepository.AddAuthorAsync(book.Author, context);
                authorId = newAuthor.Id;
            }

            var newBook = await _bookRepository.AddNewBookAsync(book, authorId, context);

            context.Database.CommitTransaction();
            context.Database.AutoTransactionBehavior = AutoTransactionBehavior.WhenNeeded;

            return new ResultHelper<BookDto>().Correct(value : newBook.Adapt<BookDto>());
        }
        catch (Exception ex)
        {
            context.Database.RollbackTransaction();
            context.Database.AutoTransactionBehavior = AutoTransactionBehavior.WhenNeeded;
            return new ResultHelper<BookDto>().Failed(book, "An error occurred while adding a new book");
        }
    }

    public async Task<List<BookDto>> GetBooksAsync()
    {
        var result = await _bookRepository.GetBooksAsync();
        return result.Adapt<List<BookDto>>();
    }
}
