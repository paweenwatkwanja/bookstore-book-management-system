using Models;

namespace Services;

public interface IBookService
{
    public Task<List<BookResponse>> GetBooksAsync();
    public Task<BookResponse> GetBookByIdAsync(string objectId);
    public Task CreateBookAsync(BookRequest bookRequest);
    public Task<BookResponse> UpdateBookAsync(string objectId, BookRequest bookRequest);
    public Task DeleteBookAsync(string objectId);
}