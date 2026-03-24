using Data;

namespace Repositories;

public interface IBookRepository
{
    public Task<List<BookEntity>?> GetBooksAsync();
    public Task<BookEntity?> GetBookByIDAsync(string objectId);
    public Task CreateBookAsync(BookEntity bookEntity);
    public Task UpdateBookAsync(BookEntity bookEntity);
    public Task DeletBookAsync(BookEntity bookEntity);
}