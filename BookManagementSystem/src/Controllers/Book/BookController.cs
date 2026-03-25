using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("api/books")]
    public async Task<IActionResult> GetBooksAsync()
    {
        List<BookResponse> responses = await _bookService.GetBooksAsync();
        return Ok(responses);
    }

    [HttpGet("api/books/{objectId}")]
    public async Task<IActionResult> GetBookByIdAsync(string objectId)
    {
        BookResponse response = await _bookService.GetBookByIdAsync(objectId);
        return Ok(response);
    }

    [HttpPost("api/books")]
    public async Task<IActionResult> CreateBookAsync([FromBody] BookRequest bookRequest)
    {
        await _bookService.CreateBookAsync(bookRequest);
        return Ok();
    }

    [HttpPut("api/books/{objectId}")]
    public async Task<IActionResult> UpdateBookAsync(string objectId, [FromBody] BookRequest bookRequest)
    {
        BookResponse response = await _bookService.UpdateBookAsync(objectId, bookRequest);
        return Ok(response);
    }

    [HttpDelete("api/books/{objectId}")]
    public async Task<IActionResult> DeleteBookAsync(string objectId)
    {   
        await _bookService.DeleteBookAsync(objectId);
        return Ok();
    }
}