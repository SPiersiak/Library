using Books.Api.Models;
using Books.Api.Repositories.Interfaces;
using Books.Api.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace TestBooksApi;
public class BookServiceTest
{
    [Fact]
    public async Task AddNewBooksAsync_WhenNewAuthor_CallAddNewAuthorAsyncOnce()
    {
        // Arrange
        var mocContext = new Mock<LibraryContext>();
        mocContext.Setup(s => s.Database).Returns(new MockDatabaseFacade(mocContext.Object));

        var mockContextFactory = new Mock<IDbContextFactory<LibraryContext>>();
        mockContextFactory
            .Setup(s => s.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(mocContext.Object);

        var mockBookRepository = new Mock<IBookRepository>();
        mockBookRepository
            .Setup(s => s.AddNewBookAsync(BookDto, Book.Id, It.IsAny<LibraryContext>()))
            .ReturnsAsync(Book);

        var mockAuthorRepository = new Mock<IAuthorRepository>();
        mockAuthorRepository.Setup(service => service.CheckIfAuthorExistAsync(BookDto.Author,It.IsAny<LibraryContext>()))
            .ReturnsAsync((false, 0));
        mockAuthorRepository.Setup(service => service.AddAuthorAsync(BookDto.Author, mocContext.Object))
            .ReturnsAsync(new Author());

        var service = new BookService(mockBookRepository.Object, mockContextFactory.Object, mockAuthorRepository.Object);

        // Act
        var result = await service.AddNewBookAsync(BookDto);

        // Asert
        mockAuthorRepository.Verify(v => v.AddAuthorAsync(BookDto.Author, mocContext.Object), Times.Once());
    }

    [Fact]
    public async Task AddNewBooksAsync_WhenExisitngAuthor_DoNotCallAddNewAuthorAsync()
    {
        // Arrange
        var mocContext = new Mock<LibraryContext>();
        mocContext.Setup(s => s.Database).Returns(new MockDatabaseFacade(mocContext.Object));

        var mockContextFactory = new Mock<IDbContextFactory<LibraryContext>>();
        mockContextFactory
            .Setup(s => s.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(mocContext.Object);

        var mockBookRepository = new Mock<IBookRepository>();
        mockBookRepository
            .Setup(s => s.AddNewBookAsync(BookDto, Book.Id, It.IsAny<LibraryContext>()))
            .ReturnsAsync(Book);

        var mockAuthorRepository = new Mock<IAuthorRepository>();
        mockAuthorRepository.Setup(service => service.CheckIfAuthorExistAsync(BookDto.Author, It.IsAny<LibraryContext>()))
            .ReturnsAsync((true, 1));
        mockAuthorRepository.Setup(service => service.AddAuthorAsync(BookDto.Author, mocContext.Object))
            .ReturnsAsync(new Author());

        var service = new BookService(mockBookRepository.Object, mockContextFactory.Object, mockAuthorRepository.Object);

        // Act
        var result = await service.AddNewBookAsync(BookDto);

        // Asert
        mockAuthorRepository.Verify(v => v.AddAuthorAsync(BookDto.Author, mocContext.Object), Times.Never());
    }

    private readonly static BookDto BookDto = new BookDto() { Title = "Test", Price = 20.54m, Author = new AuthorDto() { FirstName = "Jan", LastName = "Kowalski" } };
    private readonly static Book Book = new Book() { Author = new Author() { Id = 1, FirstName = "Jan", LastName = "Kowalski" } };
}