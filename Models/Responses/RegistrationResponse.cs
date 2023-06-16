using CIP.API.Interfaces;
using CIP.API.Models.Users;

namespace CIP.API.Models.Responses
{
    public class RegistrationResponse : ICustomResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; } = Enumerable.Empty<string>();
        public bool Success { get; set; }
        public AuthenticatedUser? AuthenticatedUser { get; set; }
    }
}
