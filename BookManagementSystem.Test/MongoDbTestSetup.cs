using MongoDB.Driver;
using Testcontainers.MongoDb;

[TestClass]
public class MongoDbTestSetup
{
    private static MongoDbContainer _container = null!;
    public static IMongoDatabase Database { get; private set; } = null!;

    [AssemblyInitialize]
    public static async Task InitializeAsync(TestContext context)
    {
        _container = new MongoDbBuilder("mongo:8.0")
            .Build();

        await _container.StartAsync();

        MongoClient client = new MongoClient(_container.GetConnectionString());
        Database = client.GetDatabase($"testdb");
    }

    [AssemblyCleanup]
    public static async Task CleanupAsync()
    {
        if (_container != null)
        {
            await _container.StopAsync();
            await _container.DisposeAsync();
        }
    }
}