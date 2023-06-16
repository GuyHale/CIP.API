using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CIP.API.Models.Users
{
    [DynamoDBTable("Users")]
    public class SignUpUser
    {
        [Required]
        [DynamoDBHashKey("Username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [DynamoDBProperty("Password")]
        public string Password { get; set; } = string.Empty;

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

        [DynamoDBProperty("Salt")]
        public string Salt { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
