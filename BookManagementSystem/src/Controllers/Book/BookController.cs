using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Global.Exceptions;

namespace Controllers;

[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("api/books")]
    public async Task<List<BookResponse>> GetBooksAsync()
    {
        List<BookResponse> responses = await _bookService.GetBooksAsync();
        return responses;
    }

    [HttpGet("api/books/{objectId}")]
    public async Task<BookResponse> GetBookByIdAsync(string objectId)
    {
        if (!MongoDB.Bson.ObjectId.TryParse(objectId, out _))
            throw new BadRequestException("Invalid ID format");
        BookResponse response = await _bookService.GetBookByIdAsync(objectId);
        return response;
    }

    [HttpPost("api/books")]
    public async Task<string?> CreateBookAsync([FromBody] BookRequest bookRequest)
    {
        return await _bookService.CreateBookAsync(bookRequest);
    }

    [HttpPut("api/books/{objectId}")]
    public async Task<BookResponse> UpdateBookAsync(string objectId, [FromBody] BookRequest bookRequest)
    {
        if (!MongoDB.Bson.ObjectId.TryParse(objectId, out _))
            throw new BadRequestException("Invalid ID format");
        BookResponse response = await _bookService.UpdateBookAsync(objectId, bookRequest);
        return response;
    }

    [HttpDelete("api/books/{objectId}")]
    public async Task DeleteBookAsync(string objectId)
    {
        if (!MongoDB.Bson.ObjectId.TryParse(objectId, out _))
            throw new BadRequestException("Invalid ID format");
        await _bookService.DeleteBookAsync(objectId);
    }
}