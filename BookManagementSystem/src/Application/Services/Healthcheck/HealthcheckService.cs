using Data;
using Models;
using Repositories;

namespace Services;

public class HealthcheckService : IHealthcheckService
{
    private readonly HealthcheckRepository _healthcheckRepository;

    public HealthcheckService(HealthcheckRepository healthcheckRepository)
    {
        _healthcheckRepository = healthcheckRepository;
    }

    public HealthcheckResponse GetHealthcheck()
    {
        HealthcheckEntity healthcheckEntity = _healthcheckRepository.GetHealthcheck();
        HealthcheckResponse healthCheckResponse = new HealthcheckResponse()
        {
            ObjectId = healthcheckEntity.ObjectId,
            Status = healthcheckEntity.Status
        };
        return healthCheckResponse;
    }
}