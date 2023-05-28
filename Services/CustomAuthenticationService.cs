using CIP.API.Helpers;
using CIP.API.Identity;
using CIP.API.Interfaces;
using CIP.API.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace CIP.API.Services
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly ILogger<CustomAuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly UserManager<ApiUser> _userManager;

        public CustomAuthenticationService(ILogger<CustomAuthenticationService> logger,
            IConfiguration configuration,
            IDapperWrapper dapperWrapper,
            IDbConnectionFactory dbConnectionFactory,
            UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _configuration = configuration;
            _dapperWrapper = dapperWrapper;
            _dbConnectionFactory = dbConnectionFactory;
            _userManager = userManager;
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

        public async Task<ICustomResponse> Register(User customUser)
        {
            try
            {
                ApiUser apiUser = TransferUser<User, ApiUser>(customUser);
                IdentityResult identityResult = await _userManager.CreateAsync(apiUser, customUser.Password);

                if (!identityResult.Succeeded)
                {
                    return ApiResponseHelpers.FailureResponse<RegistrationResponse>(identityResult.Errors);
                }
                await _userManager.AddToRoleAsync(apiUser, LookUps.Roles.User.Description());
                ApiUser apiUserDb = await _userManager.FindByNameAsync(customUser.UserName);
                User user = TransferUser<ApiUser, User>(apiUserDb);
                
                return ApiResponseHelpers.RegistrationSuccess().AddUserToResponse(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.ServerError<RegistrationResponse>();
        }

        private TDestination TransferUser<TSource, TDestination>(TSource user)
            where TSource : class
            where TDestination : class, new()
        {
            TDestination apiUser = new();

            PropertyInfo[] userProperties = user.GetType().GetProperties();
            PropertyInfo[] apiUserProperties = apiUser.GetType().GetProperties();
            foreach (PropertyInfo userProperty in userProperties)
            {
                PropertyInfo? apiUserProperty = apiUserProperties.Where(prop => prop.Name == userProperty.Name).FirstOrDefault();
                if (apiUserProperty is null)
                {
                    continue;
                }
                object? propertyValue = userProperty.GetValue(user);
                if (propertyValue is null)
                {
                    continue;
                }
                apiUserProperty.SetValue(apiUser, propertyValue, null);
            }
            return apiUser;
        }
    }
}
