using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFisrtAPIAuthorization.Services;

namespace MyFisrtAPIAuthorization.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HomeController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HomeController> _logger;
        private readonly IAutheticationService _autheticationService;

        public HomeController(ILogger<HomeController> logger,
            IAutheticationService autheticationService)
        {
            _logger = logger;
            _autheticationService = autheticationService;
        }

        [HttpGet("v1")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var name = User.Claims.ToList();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("v2")]
        public IEnumerable<WeatherForecast> GetV2()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("signin/{name}")]
        public async Task<string> Signin(string name)
        {
            return await _autheticationService.AuthenticateAsync(name, Guid.NewGuid().ToString());
        }
    }
}
