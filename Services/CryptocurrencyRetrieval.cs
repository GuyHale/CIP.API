using CIP.API.Interfaces;
using CIP.API.Models;
using Dapper;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace CIP.API.Services
{
    public class CryptocurrencyRetrieval : ICryptocurrencyRetrieval
    {
        private readonly ILogger<CryptocurrencyRetrieval> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CryptocurrencyRetrieval(ILogger<CryptocurrencyRetrieval> logger, 
            IConfiguration configuration, 
            IDapperWrapper dapperWrapper, 
            IDbConnectionFactory dbConnectionFactory)
        {
            _logger = logger;   
            _configuration = configuration;
            _dapperWrapper = dapperWrapper;
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<IEnumerable<Cryptocurrency>> Get()
        {
            string connectionString = _configuration.GetConnectionString("CryptoScraperDb") ?? string.Empty;
            using IDbConnection connection = _dbConnectionFactory.CreateConnection(connectionString);
            try
            {
                return await _dapperWrapper.QueryAsync<Cryptocurrency>(connection, "GetCryptocurrencies", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Enumerable.Empty<Cryptocurrency>();
        }

        public async Task<Cryptocurrency> Get(string name)
        {
            string connectionString = _configuration.GetConnectionString("CryptoScraperDb") ?? string.Empty;
            using IDbConnection connection = _dbConnectionFactory.CreateConnection(connectionString);
            try
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("_name", name, DbType.String);
                return await _dapperWrapper.QuerySinglOrDefaultAsync<Cryptocurrency>(connection, "GetCryptocurrencyByName", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Cryptocurrency.Empty();
        }
        
        public async Task<Cryptocurrency> Get(int rank)
        {
            string connectionString = _configuration.GetConnectionString("CryptoScraperDb") ?? string.Empty;
            using IDbConnection connection = _dbConnectionFactory.CreateConnection(connectionString);
            try
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("_rank", rank, DbType.Int16);
                return await _dapperWrapper.QuerySinglOrDefaultAsync<Cryptocurrency>(connection, "GetCryptocurrencyByRank", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Cryptocurrency.Empty();
        }
    }
}
