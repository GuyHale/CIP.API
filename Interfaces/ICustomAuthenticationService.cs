using CIP.API.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string> GetApiKey(string apiKey);
        Task<ICustomResponse> Register(SignUpUser customUser);
        Task<ICustomResponse> Login(LoginUser customUser);
    }
}
