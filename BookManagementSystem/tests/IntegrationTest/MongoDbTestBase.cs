using Repositories;
using Services;
using Controllers;
using MongoDB.Driver;

public abstract class MongoDbTestBase
{
    protected BookController Controller { get; private set; } = null!;
    protected IMongoDatabase _database => MongoDbTestSetup.Database;
    private string _collectionName = $"books_{Guid.NewGuid()}";

    [TestInitialize]
    public async Task SetUpAsync()
    {
        
        await _database.DropCollectionAsync(_collectionName);
        BookRepository respository = new BookRepository(_database, _collectionName);
        BookService service = new BookService(respository);
        Controller = new BookController(service);
    }

    [TestCleanup]
    public async Task TearDownAsync()
    {
        await _database.DropCollectionAsync(_collectionName);
    }
}