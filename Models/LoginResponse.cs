using CIP.API.Interfaces;

namespace CIP.API.Models
{
    public class LoginResponse : ICustomResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; } = Enumerable.Empty<string>();
        public bool Success { get; set; }
    }
}
