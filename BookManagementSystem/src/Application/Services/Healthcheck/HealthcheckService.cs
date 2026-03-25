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
        HealthcheckEntity healthcheckEntity = await _healthcheckRepository.GetHealthcheckAsync();
        HealthcheckResponse healthCheckResponse = new HealthcheckResponse()
        {
            ObjectId = healthcheckEntity.ObjectId,
            Status = healthcheckEntity.Status
        };
        return healthCheckResponse;
    }
}