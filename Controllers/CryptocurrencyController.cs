using CIP.API.Interfaces;
using CIP.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Controllers
{
    [Route("cip/cryptocurrencies")]
    [ApiController]
    public class CryptocurrencyController : ControllerBase
    {
        private readonly ICryptocurrencyRetrieval _cryptocurrencyRetrieval;
        private readonly ILogger<CryptocurrencyController> _logger;

        public CryptocurrencyController(ICryptocurrencyRetrieval cryptocurrencyRetrieval, ILogger<CryptocurrencyController> logger)
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
                var res = await _cryptocurrencyRetrieval.Get();
                _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(res));
                return res;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Enumerable.Empty<Cryptocurrency>();
        }
    }
}
