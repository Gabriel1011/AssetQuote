using AssetQuote.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IEnumerable<Asset>> Get()
        {
            var teste = await _assetService.AddAsset(new Asset("Name"));

            var all = await _assetService.GetAllAssets();

            return await Task.FromResult(all);
        }
    }
}
