using CIP.API.Identity;
using CIP.API.Interfaces;
using CIP.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Controllers
{
    [Authorize]
    [Route("cip/api")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ICryptocurrencyRetrieval _cryptocurrencyRetrieval;
        private readonly ICustomAuthenticationService _customAuthenticationService;
        private readonly ILogger<DeveloperController> _logger;
        private readonly UserManager<ApiUser> _userManager;

        public DeveloperController(ICryptocurrencyRetrieval cryptocurrencyRetrieval, ICustomAuthenticationService customAuthenticationService, ILogger<DeveloperController> logger, UserManager<ApiUser> userManager)
        {
            _cryptocurrencyRetrieval = cryptocurrencyRetrieval;
            _customAuthenticationService = customAuthenticationService;
            _logger = logger;
            _userManager = userManager;
        }

        [Route("{apiKey}/get/all")]
        [HttpGet]
        public async Task<IEnumerable<Cryptocurrency>> Get(string apiKey)
        {
            try
            {
                if (!await ApiKeyValidation(apiKey))
                {
                    return Enumerable.Empty<Cryptocurrency>();
                }
                return await _cryptocurrencyRetrieval.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Enumerable.Empty<Cryptocurrency>();
        }

        [Route("{apiKey}/get/{name}")]
        [HttpGet]
        public async Task<Cryptocurrency> Get(string apiKey, string name)
        {
            try
            {
                if (!await ApiKeyValidation(apiKey))
                {
                    return Cryptocurrency.Empty();
                }
                return await _cryptocurrencyRetrieval.Get(name);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Cryptocurrency.Empty();
        }
        
        [Route("{apiKey}/get/{rank}")]
        [HttpGet]
        public async Task<Cryptocurrency> Get(string apiKey, int rank)
        {
            try
            {
                if (!await ApiKeyValidation(apiKey))
                {
                    return Cryptocurrency.Empty();
                }
                return await _cryptocurrencyRetrieval.Get(rank);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Cryptocurrency.Empty();
        }

        private async Task<bool> ApiKeyValidation(string apiKey)
        {
            return string.IsNullOrEmpty(await _customAuthenticationService.GetApiKey(apiKey));
        }
    }
}
