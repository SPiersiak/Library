using Books.Api.Models;
using Books.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;
[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var result = await _bookService.GetBooksAsync();

        return Ok(result);
    }
}
