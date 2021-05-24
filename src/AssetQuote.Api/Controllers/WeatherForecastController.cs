using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AssetQuote.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAssetService _assetService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAssetService assetService)
        {
        _logger = logger;
        _assetService = assetService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var teste = await _assetService.AddAsset(new Asset("Name"));

            var all = await _assetService.GetAllAssets();

            var rng = new Random();
            var tt = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return await Task.FromResult(tt);
        }
    }
}
