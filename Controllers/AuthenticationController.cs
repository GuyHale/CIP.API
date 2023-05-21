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
                    return FailureResponse(identityResult.Errors);
                }
                await _userManager.AddToRoleAsync(apiUser, LookUps.Roles.User.Description());
                return SuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MethodName", MethodBase.GetCurrentMethod()?.Name);
            }
            return ServerError();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            try
            {
                ApiUser customUser = await _userManager.FindByNameAsync(user.Username);
                bool passwordValid = await _userManager.CheckPasswordAsync(customUser, user.Password);

                if (customUser is null || !passwordValid)
                {
                    return NotFound();
                }
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MethodName", MethodBase.GetCurrentMethod()?.Name);
            }
            return Problem(MethodBase.GetCurrentMethod()?.Name, statusCode: 500);
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

        private ICustomResponse FailureResponse(IEnumerable<IdentityError> identityErrors)
        {
            RegistrationResponse registrationResponse = new();
            registrationResponse.Success = false;
            registrationResponse.ErrorMessages = identityErrors.Select(x => x.Description);
            
            return registrationResponse;
        }

        private ICustomResponse SuccessResponse()
        {
            return
            new RegistrationResponse()
            {
                Success = true
            };
        }

        private ICustomResponse ServerError()
        {
            return  new RegistrationResponse() { Success = false, ErrorMessages = new string[] { "Sorry something unexpected has happened, please try again" } } ;
        }

    }
}
