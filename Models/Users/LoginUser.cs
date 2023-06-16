using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;

namespace CIP.API.Models.Users
{
    [DynamoDBTable("Users")]
    public class LoginUser
    {
        [Required]
        [DynamoDBProperty("Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [DynamoDBProperty("Password")]
        public string Password { get; set; } = string.Empty;
    }
}
