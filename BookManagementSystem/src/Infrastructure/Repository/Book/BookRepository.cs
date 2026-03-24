using Data;
using MongoDB.Driver;

namespace Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookManagementSystemDbContext _bookManagementSystemDbContext;

    public BookRepository(BookManagementSystemDbContext bookManagementSystemDbContext)
    {
        _bookManagementSystemDbContext = bookManagementSystemDbContext;
    }

    public async Task<List<BookEntity>?> GetBooksAsync(){
        FilterDefinition<BookEntity> filter = Builders<BookEntity>.Filter.Empty;
        return await _bookManagementSystemDbContext.FindAsync<List<BookEntity>>(filter);
    }

    public async Task<BookEntity?> GetBookByIDAsync(string objectId){
        FilterDefinition<BookEntity> filter = Builders<BookEntity>.Filter.Eq(b => b.ObjectId, objectId);
        return await _bookManagementSystemDbContext.FindAsync<BookEntity>(filter);
    }

    public async Task CreateBookAsync(BookEntity bookEntity){
        await _bookManagementSystemDbContext.AddAsync<BookEntity>(bookEntity);
        await _bookManagementSystemDbContext.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(BookEntity bookEntity){
        _bookManagementSystemDbContext.Update<BookEntity>(bookEntity);
        await _bookManagementSystemDbContext.SaveChangesAsync();
    }

     public async Task DeletBookAsync(BookEntity bookEntity){
        _bookManagementSystemDbContext.Books.Remove(bookEntity);
        await _bookManagementSystemDbContext.SaveChangesAsync();
    }
}