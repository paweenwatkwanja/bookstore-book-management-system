using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

public class HealthcheckController : Controller
{
    private readonly HealthcheckService _healthcheckService;

    public HealthcheckController(HealthcheckService healthcheckService)
    {
        _healthcheckService = healthcheckService;
    }

    [HttpGet("/healthchecks")]
    public async Task<IActionResult> GetHealthcheck()
    {
        return Ok(_healthcheckService.GetHealthcheck());
    }
}