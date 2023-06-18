using Amazon.DynamoDBv2.DataModel;
using CIP.API.Helpers;
using CIP.API.Interfaces;
using CIP.API.Models;
using CIP.API.Models.Responses;
using System.Data.Entity.Infrastructure;

namespace CIP.API.Services
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly ILogger<CustomAuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDynamoDBContext _dynamoDBContext;

        public CustomAuthenticationService(ILogger<CustomAuthenticationService> logger,
            IConfiguration configuration,
            IDapperWrapper dapperWrapper,
            IDbConnectionFactory dbConnectionFactory,
            IDynamoDBContext dynamoDBContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dapperWrapper = dapperWrapper;
            _dbConnectionFactory = dbConnectionFactory;
            _dynamoDBContext = dynamoDBContext;
        }
        public async Task<bool> VerifyApiKey(string apiKeyString)
        {
            try
            {
                ApiKey? apiKey = await _dynamoDBContext.LoadAsync<ApiKey>(apiKeyString);
                return apiKey is not null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
                throw new ArgumentException(ex.Message);
            }          
        }

        public async Task<ICustomResponse> CreateApiKey(string apiKeyString)
        {
            try
            {
                ApiKey apiKey = new() { Key = apiKeyString };
                await _dynamoDBContext.SaveAsync(apiKey);
                return await VerifyApiKey(apiKeyString) ? ApiResponseHelpers.SuccessResponse<ApiKeyCreationResponse>() : ApiResponseHelpers.ServerError<ApiKeyCreationResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
