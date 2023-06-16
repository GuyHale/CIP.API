using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;

namespace CIP.API.Models.Users
{
    public class AuthenticatedUser
    {
        [Required]
        [DynamoDBHashKey("Username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DynamoDBProperty("Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DynamoDBProperty("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [DynamoDBProperty("FirstName")]
        public string LastName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
