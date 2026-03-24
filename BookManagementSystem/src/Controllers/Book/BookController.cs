using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("api/")]
public class BookController : Controller
{
    private readonly BookService _bookService;
    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("/books")]
    public async Task<IActionResult> GetBooksAsync()
    {
        return Ok(await _bookService.GetBooksAsync());
    }

    [HttpGet("/books/{objectId}")]
    public async Task<IActionResult> GetBookByIdAsync(string objectId)
    {
        return Ok(await _bookService.GetBookByIdAsync(objectId));
    }

    [HttpPost("/books")]
    public async Task<IActionResult> CreateBookAsync([FromBody] BookRequest bookRequest)
    {
        return Ok(_bookService.CreateBookAsync(bookRequest));
    }

    [HttpPut("/books/{objectId}")]
    public async Task<IActionResult> UpdateBookAsync(string objectId, [FromBody] BookRequest bookRequest)
    {
        return Ok(_bookService.UpdateBookAsync(objectId, bookRequest));
    }

    [HttpDelete("/books/{objectId}")]
    public async Task<IActionResult> DeleteBookAsync(string objectId)
    {       
        return Ok(_bookService.DeleteBookAsync(objectId));
    }
}