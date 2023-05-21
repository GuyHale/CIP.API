using CIP.API.Helpers;
using CIP.API.Identity;
using CIP.API.Interfaces;
using CIP.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

namespace CIP.API.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserManager<ApiUser> _userManager;

        public AuthenticationController(ILogger<AuthenticationController> logger, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ICustomResponse> Register([FromBody] CustomUser customUser)
        {
            try
            {
                ApiUser apiUser = TransferUser<CustomUser, ApiUser>(customUser);
                IdentityResult identityResult = await _userManager.CreateAsync(apiUser, customUser.Password);

                if(!identityResult.Succeeded)
                {
                    return ApiResponseHelpers.FailureResponse<RegistrationResponse>(identityResult.Errors);
                }
                await _userManager.AddToRoleAsync(apiUser, LookUps.Roles.User.Description());
                return ApiResponseHelpers.SuccessResponse<RegistrationResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MethodName", MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.ServerError<RegistrationResponse>();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ICustomResponse> Login(LoginUser user)
        {
            try
            {
                ApiUser customUser = await _userManager.FindByNameAsync(user.Username);
                bool passwordValid = await _userManager.CheckPasswordAsync(customUser, user.Password);

                if (customUser is null || !passwordValid)
                {
                    return ApiResponseHelpers.LoginFailure<LoginResponse>();
                }
                return ApiResponseHelpers.SuccessResponse<LoginResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MethodName", MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.ServerError<LoginResponse>();
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
