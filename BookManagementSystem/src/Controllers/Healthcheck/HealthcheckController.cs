using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

public class HealthcheckController : Controller
{
    private readonly IHealthcheckService _healthcheckService;

    public HealthcheckController(IHealthcheckService healthcheckService)
    {
        _healthcheckService = healthcheckService;
    }

    [HttpGet("/healthchecks")]
    public async Task<IActionResult> GetHealthcheck()
    {
        return Ok(await _healthcheckService.GetHealthcheckAsync());
    }
}