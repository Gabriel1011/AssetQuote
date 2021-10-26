using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetQuote.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssetQuoteController : ControllerBase
    {

        private readonly ILogger<AssetQuoteController> _logger;
        private readonly IAssetService _assetService;

        public AssetQuoteController(ILogger<AssetQuoteController> logger, IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        [HttpGet]
        public async Task<IEnumerable<Asset>> Get()
        {
            var all = await _assetService.GetAllAssets();

            return await Task.FromResult(all);
        }
    }
}
