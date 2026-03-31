using Data;

namespace Repositories;

public interface IHealthcheckRepository
{
  public Task<HealthcheckEntity> GetHealthcheckAsync();
}