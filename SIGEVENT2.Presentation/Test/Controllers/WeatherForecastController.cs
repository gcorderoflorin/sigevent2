// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using SIGEVENT2.Core.Membership.Data;
// using SIGEVENT2.Models;
// using SIGEVENT2.Services;

// namespace SIGEVENT2.Presentation.Test.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     [Authorize(Roles = InitialRoles.SuperAdmin)]
//     public class WeatherForecastController : ControllerBase
//     {
//         private readonly WeatherForecastService _service;

//         public WeatherForecastController(WeatherForecastService service)
//         {
//             _service = service;
//         }

//         [HttpGet]
//         public async Task<IEnumerable<WeatherForecast>> Get()
//         {
//             return await _service.GetForecastsAsync();
//         }

//         [HttpPost]
//         public async Task<IActionResult> Post(WeatherForecast forecast)
//         {
//             await _service.AddForecastAsync(forecast);
//             return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
//         }
//     }
// }
