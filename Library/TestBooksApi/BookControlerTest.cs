using Books.Api.Controllers;
using Books.Api.Models;
using Books.Api.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestBooksApi;

public class BookControlerTest
{
    [Fact]
    public async Task GetBooks_WhenSuccess_ReturnsCode200()
    {
        var mockBookService = new Mock<IBookService>();
        var controller = new BookController(mockBookService.Object);

        var result = (OkObjectResult)await controller.GetBooks();

        result.StatusCode.Should().Be(200);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetBooks_WhenSuccess_ReturnsBookList()
    {
        var mockBookService = new Mock<IBookService>();
        mockBookService
            .Setup(service => service.GetBooksAsync())
            .ReturnsAsync(new List<BookDto>());
        var controller = new BookController(mockBookService.Object);

        var result = (OkObjectResult)await controller.GetBooks();

        result.StatusCode.Should().Be(200);
        result.Should().BeOfType<OkObjectResult>();
        result.Value.Should().BeOfType<List<BookDto>>();
    }

    [Fact]
    public async Task GetBooks_WhenSuccess_ReturnsAllBookList()
    {
        var mockBookService = new Mock<IBookService>();
        mockBookService
            .Setup(service => service.GetBooksAsync())
            .ReturnsAsync(Books);
        var controller = new BookController(mockBookService.Object);

        var result = (OkObjectResult)await controller.GetBooks();

        var value = Assert.IsType<List<BookDto>>(result.Value);
        Assert.Equal(Books.Count, value.Count);
    }

    [Fact]
    public async Task AddBook_WhenInvalidModel_ReturnsStatus422()
    {
        var mockBookService = new Mock<IBookService>();
        var controller = new BookController(mockBookService.Object);
        var model = Books.First();
        controller.ModelState.AddModelError("fakeError", "fakeError");

        var result = (UnprocessableEntityObjectResult)await controller.AddBook(model);

        result.StatusCode.Should().Be(422);
        result.Should().BeOfType<UnprocessableEntityObjectResult>();
    }

    [Fact]
    public async Task AddBook_WhenError_ReturnsStatus500()
    {
        var model = Books.First();
        var errorMessage = "fakeError";
        var mockBookService = new Mock<IBookService>();
        mockBookService
            .Setup(service => service.AddNewBookAsync(model))
            .ReturnsAsync(new Commons.ResultHelper.ResultHelper<BookDto>().Failed(model, errorMessage));
        var controller = new BookController(mockBookService.Object);

        var result = (ObjectResult)await controller.AddBook(model);

        result.StatusCode.Should().Be(500);
        result.Value.Should().BeOfType<string>();
        Assert.Equal(errorMessage, result.Value);
    }

    [Fact]
    public async Task AddBook_WhenSuccess_ReturnsStatus201()
    {
        var model = Books.First();
        var mockBookService = new Mock<IBookService>();
        mockBookService
            .Setup(service => service.AddNewBookAsync(model))
            .ReturnsAsync(new Commons.ResultHelper.ResultHelper<BookDto>().Correct(model));
        var controller = new BookController(mockBookService.Object);

        var result = (CreatedAtActionResult)await controller.AddBook(model);

        result.StatusCode.Should().Be(201);
        result.Should().BeOfType<CreatedAtActionResult>();
        result.Value.Should().NotBeNull();
    }

    private static readonly List<BookDto> Books = new List<BookDto>()
        {
            new BookDto() { Id = 1, Title = "Test", Price = 20.54m, Author = new AuthorDto() { FirstName = "Jan", LastName = "Kowalski" } },
            new BookDto() { Id = 2, Title = "Test2", Price = 230.54m, Author = new AuthorDto() { FirstName = "Iwona", LastName = "Nowak" }},
            new BookDto() { Id = 3, Title = "Test3", Price = 50.54m, Author = new AuthorDto() { FirstName = "Leon", LastName = "Zawodowiec" }},
        };
}