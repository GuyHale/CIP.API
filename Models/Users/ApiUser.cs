using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CIP.API.Models.Users
{
    public class ApiUser : IdentityUser
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
