using Models;

namespace Services;

public interface IHealthcheckService
{
  public Task<HealthcheckResponse> GetHealthcheckAsync();
}