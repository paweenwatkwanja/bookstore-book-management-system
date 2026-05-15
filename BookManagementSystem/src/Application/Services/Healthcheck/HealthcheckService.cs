using Data;
using Models;
using Repositories;

namespace Services;

public class HealthcheckService : IHealthcheckService
{
    private readonly IHealthcheckRepository _healthcheckRepository;

    public HealthcheckService(IHealthcheckRepository healthcheckRepository)
    {
        _healthcheckRepository = healthcheckRepository;
    }

    public async Task<HealthcheckResponse> GetHealthcheckAsync()
    {
        try
        {
            HealthcheckEntity? healthcheckEntity = await _healthcheckRepository.GetHealthcheckAsync();
            if (healthcheckEntity == null)
            {
                throw new Exception("No healthcheck record found in database.");
            }

            HealthcheckResponse healthCheckResponse = new HealthcheckResponse()
            {
                ObjectId = healthcheckEntity.ObjectId,
                Status = healthcheckEntity.Status
            };
            return healthCheckResponse;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving healthcheck: {ex.Message}", ex);
        }
    }
}