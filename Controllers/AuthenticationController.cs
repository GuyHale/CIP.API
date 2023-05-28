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
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, UserManager<ApiUser> userManager, ICustomAuthenticationService customAuthenticationService)
        {
            _logger = logger;
            _userManager = userManager;
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ICustomResponse> Register([FromBody] User customUser)
        {
            try
            {               
                return await _customAuthenticationService.Register(customUser);
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
    }
}
