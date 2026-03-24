using Data;

namespace Repositories;

public interface IHealthcheckRepository
{
    public HealthcheckEntity GetHealthcheck();
}