using Data;
using MongoDB.Driver;

namespace Repositories;

public class HealthcheckRepository : IHealthcheckRepository
{
    private readonly IMongoCollection<HealthcheckEntity> _healthcheckCollection;
    private const string collectionName = "healthchecks";
    
    public HealthcheckRepository(IMongoDatabase database)
    {
        _healthcheckCollection = database.GetCollection<HealthcheckEntity>(collectionName);
    }

    public async Task<HealthcheckEntity> GetHealthcheckAsync()
    {
        FilterDefinition<HealthcheckEntity> filter = Builders<HealthcheckEntity>.Filter.Empty;
        IAsyncCursor<HealthcheckEntity> cursor = await _healthcheckCollection.FindAsync<HealthcheckEntity>(filter);
        return await cursor.FirstOrDefaultAsync();
    }
}