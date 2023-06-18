using CIP.API.Helpers;
using CIP.API.Interfaces;
using CIP.API.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Controllers
{
    [Route("cip/apikey")]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;
        private readonly ILogger<ApiKeyController> _logger;

        public ApiKeyController(ICustomAuthenticationService customAuthenticationService, ILogger<ApiKeyController> logger)
        {
            _customAuthenticationService = customAuthenticationService;
            _logger = logger;
        }

        [Route("create/{apiKey}")]
        [HttpGet]
        public async Task<ICustomResponse> CreateApiKey(string apiKey)
        {
            try
            {
                return await _customAuthenticationService.CreateApiKey(apiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.ServerError<ApiKeyCreationResponse>();
        }
    }
}
