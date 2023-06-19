using CIP.API.Interfaces;

namespace CIP.API.Models.Responses
{
    public class ApiKeyCreationResponse : ICustomResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; } = Enumerable.Empty<string>();
    }
}
