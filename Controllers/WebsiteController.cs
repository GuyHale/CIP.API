using CIP.API.Interfaces;
using CIP.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Controllers
{
    [Route("cip/cryptocurrencies")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly ICryptocurrencyRetrieval _cryptocurrencyRetrieval;
        private readonly ILogger<WebsiteController> _logger;

        public WebsiteController(ICryptocurrencyRetrieval cryptocurrencyRetrieval, ILogger<WebsiteController> logger)
        {
            _cryptocurrencyRetrieval = cryptocurrencyRetrieval;
            _logger = logger;
        }

        [Route("get")]
        [HttpGet]
        public async Task<IEnumerable<Cryptocurrency>> Get()
        {
            try
            {
                return (await _cryptocurrencyRetrieval.Get()).OrderBy(x => x.Rank);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Enumerable.Empty<Cryptocurrency>();
        }
    }
}
