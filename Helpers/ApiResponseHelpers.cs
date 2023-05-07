using CIP.API.Models;
using System.Runtime.CompilerServices;

namespace CIP.API.Helpers
{
    public static class ApiResponseHelpers
    {
        public static ApiResponse InvalidApiKey(this ApiResponse apiResponse)
        {
            apiResponse.RequestError = "Invalid API key";
            apiResponse.RequestStatusCode = 404;
            apiResponse.Cryptocurrencies = Enumerable.Empty<Cryptocurrency>();
            return apiResponse;
        }

        public static ApiResponse InternalError(this ApiResponse apiResponse)
        {
            apiResponse.RequestError = "Something has gone wrong, please try again in a few minutes";
            apiResponse.RequestStatusCode = 500;
            apiResponse.Cryptocurrencies = Enumerable.Empty<Cryptocurrency>();
            return apiResponse;
        }

        public static ApiResponse NotFound(this ApiResponse apiResponse, string cryptocurrencyName)
        {
            apiResponse.RequestError = "Cryptocurrency: {Name} not found, please try a different request";
            apiResponse.RequestStatusCode = 404;
            apiResponse.Cryptocurrencies = Enumerable.Empty<Cryptocurrency>();
            return apiResponse;
        }
        
        public static ApiResponse NotFound(this ApiResponse apiResponse, int rank)
        {
            apiResponse.RequestError = "Rank: {Rank} not found, please use 1 <= rank <= 100";
            apiResponse.RequestStatusCode = 404;
            apiResponse.Cryptocurrencies = Enumerable.Empty<Cryptocurrency>();
            return apiResponse;
        }

        public static ApiResponse Success(this ApiResponse apiResponse, IEnumerable<Cryptocurrency> data)
        {
            apiResponse.RequestStatusCode = 200;
            apiResponse.Cryptocurrencies = data;
            apiResponse.IsValid = true;
            return apiResponse;
        }
    }
}
