using CIP.API.Helpers;
using CIP.API.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

namespace CIP.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                ApiUser apiUser = TransferUser<User, ApiUser>(user);
                IdentityResult identityResult = await _userManager.CreateAsync(apiUser, user.Password);

                if (!identityResult.Succeeded)
                {
                    foreach (IdentityError identityError in identityResult.Errors)
                    {
                        ModelState.AddModelError(identityError.Code, identityError.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRoleAsync(apiUser, LookUps.Roles.User.Description());
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MethodName", MethodBase.GetCurrentMethod()?.Name);
            }
            return Problem(MethodBase.GetCurrentMethod()?.Name, statusCode: 500);
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            try
            {
                ApiUser apiUser = await _userManager.FindByNameAsync(user.Username);
                bool passwordValid = await _userManager.CheckPasswordAsync(apiUser, user.Password);

                if (apiUser is null || !passwordValid)
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
            foreach(PropertyInfo userProperty in userProperties)
            {
                if(!apiUserProperties.Select(x => x.Name).Contains(userProperty.Name))
                {
                    continue;
                }
                object? propertyValue = userProperty.GetValue(user);
                if(propertyValue is null)
                {
                    continue;
                }
                userProperty.SetValue(apiUser, propertyValue, null);
            }
            return apiUser; 
        }
    }
}
