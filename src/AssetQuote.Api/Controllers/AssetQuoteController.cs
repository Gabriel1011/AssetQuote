using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        [HttpPost]
        public async Task CreateAssets(IEnumerable<Asset> Assets)
        {
            foreach (var asset in Assets)
            {
                await _assetService.FindOrCreateByCode(asset);
            }
        }
    }
}
