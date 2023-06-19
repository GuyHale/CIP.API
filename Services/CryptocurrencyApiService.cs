using CIP.API.Interfaces;
using CIP.API.Models.Responses;
using CIP.API.Models;
using CIP.API.Helpers;

namespace CIP.API.Services
{
    public class CryptocurrencyApiService : ICryptocurrencyApi
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;
        private readonly ICryptocurrencyRetrieval _cryptocurrencyRetrieval;
        private readonly ILogger<CryptocurrencyApiService> _logger;

        public CryptocurrencyApiService(ICustomAuthenticationService customAuthenticationService, ICryptocurrencyRetrieval cryptocurrencyRetrieval, ILogger<CryptocurrencyApiService> logger)
        {
            _customAuthenticationService = customAuthenticationService;
            _cryptocurrencyRetrieval = cryptocurrencyRetrieval;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<Cryptocurrency>>> GetAll(string apiKey)
        {
            try
            {
                bool validApi = await _customAuthenticationService.VerifyApiKey(apiKey);
                if (!validApi)
                {
                    return ApiResponseHelpers.InvalidApiKey();
                }
                IEnumerable<Cryptocurrency> cryptocurrencies = await _cryptocurrencyRetrieval.Get();
                return ApiResponseHelpers.Success(cryptocurrencies);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.InternalError();
        }
    }
}
