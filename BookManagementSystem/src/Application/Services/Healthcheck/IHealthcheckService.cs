using Models;

namespace Services;

public interface IHealthcheckService
{
  public HealthcheckResponse GetHealthcheck();
}