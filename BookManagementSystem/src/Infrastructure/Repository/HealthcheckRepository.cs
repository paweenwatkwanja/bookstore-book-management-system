using Data;

namespace Repositories;

public class HealthcheckRepository
{
    private readonly BookManagementSystemDbContext _bookManagementSystemDbContext;

    public HealthcheckRepository(BookManagementSystemDbContext bookManagementSystemDbContext)
    {
        _bookManagementSystemDbContext = bookManagementSystemDbContext;
    }

    public HealthcheckEntity GetHealthcheck()
    {
        return _bookManagementSystemDbContext.Healthchecks.FirstOrDefault();
    }
}