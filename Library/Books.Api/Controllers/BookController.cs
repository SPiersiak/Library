using Azure;
using Books.Api.Models;
using Books.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        var result = await _bookService.GetBooksAsync();

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddBook([FromBody] BookDto book)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        var result = await _bookService.AddNewBookAsync(book);

        return result.Sucess
            ? CreatedAtAction(nameof(AddBook), new { id = result.Value.Id }, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }
}
