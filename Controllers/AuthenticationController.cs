using CIP.API.Helpers;
using CIP.API.Interfaces;
using CIP.API.Models.Responses;
using CIP.API.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

namespace CIP.API.Controllers
{
    [Route("cip/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, ICustomAuthenticationService customAuthenticationService)
        {
            _logger = logger;
            _customAuthenticationService = customAuthenticationService;
        }
        
        [Route("register")]
        [HttpPost]
        public async Task<ICustomResponse> Register([FromBody] SignUpUser signUpUser)
        {
            try
            {               
                return await _customAuthenticationService.Register(signUpUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.ServerError<RegistrationResponse>();
        }
        
        [Route("login")]
        [HttpPost]
        public async Task<ICustomResponse> Login(LoginUser loginUser)
        {
            try
            {
                return await _customAuthenticationService.Login(loginUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.ServerError<LoginResponse>();
        }             
    }
}
