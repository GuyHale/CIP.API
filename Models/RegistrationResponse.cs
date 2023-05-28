using CIP.API.Identity;
using CIP.API.Interfaces;

namespace CIP.API.Models
{
    public class RegistrationResponse : ICustomResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; } = Enumerable.Empty<string>();
        public bool Success { get; set; }
        public User? User { get; set; }
    }
}
