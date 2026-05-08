using Data;
using MongoDB.Driver;

namespace Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<BookEntity> _bookCollection;
    private string _collectionName = "books";

    public BookRepository(IMongoDatabase database, string? collectionName = null)
    {
        if (collectionName != null)
        {
            _collectionName = collectionName;
        }
        _bookCollection = database.GetCollection<BookEntity>(_collectionName);
    }

    public async Task<List<BookEntity>?> GetBooksAsync(){
        FilterDefinition<BookEntity> filter = Builders<BookEntity>.Filter.Empty;
        IAsyncCursor<BookEntity> cursor = await _bookCollection.FindAsync<BookEntity>(filter);
        List<BookEntity> bookEntities =  await cursor.ToListAsync();
        return bookEntities;
    }

    public async Task<BookEntity?> GetBookByIDAsync(string objectId){
        FilterDefinition<BookEntity> filter = Builders<BookEntity>.Filter.Eq(b => b.ObjectId, objectId);
        IAsyncCursor<BookEntity> cursor = await _bookCollection.FindAsync<BookEntity>(filter);
        return await cursor.FirstOrDefaultAsync();
    }

    public async Task<string?> CreateBookAsync(BookEntity bookEntity){
        await _bookCollection.InsertOneAsync(bookEntity);
        return bookEntity.ObjectId?.ToString();
    }

    public async Task<BookEntity> UpdateBookAsync(string objectId, BookEntity bookEntity){
        FilterDefinition<BookEntity> filter = Builders<BookEntity>.Filter.Eq(b => b.ObjectId, objectId);
        UpdateDefinition<BookEntity> update =Builders<BookEntity>.Update
            .Set(b => b.Title, bookEntity.Title)
            .Set(b => b.Author, bookEntity.Author)
            .Set(b => b.ISBN, bookEntity.ISBN)
            .Set(b => b.Publisher, bookEntity.Publisher)
            .Set(b => b.PublicationDate, bookEntity.PublicationDate);

        FindOneAndUpdateOptions<BookEntity, BookEntity> options = new FindOneAndUpdateOptions<BookEntity, BookEntity>{
            ReturnDocument = ReturnDocument.After
        };

        BookEntity updatedBook = await _bookCollection.FindOneAndUpdateAsync<BookEntity>(filter, update, options);
        return updatedBook;
    }
        
     public async Task DeleteBookAsync(string objectId){
        await _bookCollection.DeleteOneAsync<BookEntity>(b => b.ObjectId == objectId);
    }
}