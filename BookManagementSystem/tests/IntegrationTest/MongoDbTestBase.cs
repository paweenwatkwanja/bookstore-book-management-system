using Repositories;
using Services;
using Controllers;
using MongoDB.Driver;

public abstract class MongoDbTestBase
{
    protected BookController Controller { get; private set; } = null!;
    protected IMongoDatabase _database => MongoDbTestSetup.Database;

    [TestInitialize]
    public async Task SetUpAsync()
    {
        await _database.DropCollectionAsync("books");
        string collectionName = $"books_{Guid.NewGuid()}";
        BookRepository respository = new BookRepository(_database, collectionName);
        BookService service = new BookService(respository);
        Controller = new BookController(service);
    }

    [TestCleanup]
    public async Task TearDownAsync()
    {
        // Option A — drop after each test (mirror of SetUp)
        await _database.DropCollectionAsync("books");

        // Option B — dispose any test-specific resources
        // e.g. if Repository implemented IDisposable
        // Repository?.Dispose();
    }
}