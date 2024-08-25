using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Transaction.service.Controllers;

[ApiController]
[Route("/api/transaction")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "hello from transaction";
    }
}
