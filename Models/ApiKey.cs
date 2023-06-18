using Amazon.DynamoDBv2.DataModel;

namespace CIP.API.Models
{
    [DynamoDBTable("Apikeys")]
    public class ApiKey
    {
        [DynamoDBHashKey("Key")]
        public string Key { get; set; } = string.Empty;

        public ApiKey(string key)
        {
            Key = key;
        }

        public static ApiKey Create(string key) => new(key);
    }
}
