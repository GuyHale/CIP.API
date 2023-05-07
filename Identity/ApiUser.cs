using Microsoft.AspNetCore.Identity;

namespace CIP.API.Identity
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ApiKey => Guid.NewGuid().ToString();
    }
}
