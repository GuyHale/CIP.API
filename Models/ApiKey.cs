using Amazon.DynamoDBv2.DataModel;

namespace CIP.API.Models
{
    [DynamoDBTable("ApiKeys")]
    public class ApiKey
    {
        [DynamoDBHashKey("Key")]
        public string Key { get; set; } = string.Empty;
    }
}
