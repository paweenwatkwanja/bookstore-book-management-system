using Data;
using MongoDB.Driver;

namespace Repositories;

public interface IBookRepository
{
    public Task<List<BookEntity>?> GetBooksAsync();
    public Task<BookEntity?> GetBookByIDAsync(string objectId);
    public Task<string?> CreateBookAsync(BookEntity bookEntity);
    public Task<BookEntity> UpdateBookAsync(string objectId, BookEntity bookEntity);
    public Task<DeleteResult> DeleteBookAsync(string objectId);
}