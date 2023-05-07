using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string> GetApiKey(string apiKey);
    }
}
