using CIP.API.Interfaces;
using CIP.API.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace CIP.API.Services
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly ILogger<CustomAuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CustomAuthenticationService(ILogger<CustomAuthenticationService> logger,
            IConfiguration configuration,
            IDapperWrapper dapperWrapper,
            IDbConnectionFactory dbConnectionFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _dapperWrapper = dapperWrapper;
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<string> GetApiKey(string apiKey)
        {
            string connectionString = _configuration.GetConnectionString("IdentityDb") ?? string.Empty;
            using IDbConnection connection = _dbConnectionFactory.CreateConnection(connectionString);
            try
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("_apiKey", apiKey, DbType.String);
                return await _dapperWrapper.QuerySinglOrDefaultAsync<string>(connection, "IdentityDb", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return string.Empty;
        }
    }
}
