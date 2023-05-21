using CIP.API.Models;
using System.Runtime.CompilerServices;

namespace CIP.API.Helpers
{
    public static class ApiResponseHelpers
    {
        public static ApiResponse<IEnumerable<Cryptocurrency>> InvalidApiKey(this ApiResponse<IEnumerable<Cryptocurrency>> apiResponse)
        {
            apiResponse.RequestError = "Invalid API key";
            apiResponse.RequestStatusCode = 404;
            apiResponse.DbResult = new DbResult<IEnumerable<Cryptocurrency>>(){ Data = Enumerable.Empty<Cryptocurrency>() };
            return apiResponse;
        }

        public static ApiResponse<IEnumerable<Cryptocurrency>> InternalError(this ApiResponse<IEnumerable<Cryptocurrency>> apiResponse)
        {
            apiResponse.RequestError = "Something has gone wrong, please try again in a few minutes";
            apiResponse.RequestStatusCode = 500;
            apiResponse.DbResult = new DbResult<IEnumerable<Cryptocurrency>>(){ Data = Enumerable.Empty<Cryptocurrency>() };
            return apiResponse;
        }

        public static ApiResponse<IEnumerable<Cryptocurrency>> NotFound(this ApiResponse<IEnumerable<Cryptocurrency>> apiResponse, string cryptocurrencyName)
        {
            apiResponse.RequestError = "Cryptocurrency: {Name} not found, please try a different request";
            apiResponse.RequestStatusCode = 404;
            apiResponse.DbResult = new DbResult<IEnumerable<Cryptocurrency>>() { Data = Enumerable.Empty<Cryptocurrency>() };
            return apiResponse;
        }
        
        public static ApiResponse<IEnumerable<Cryptocurrency>> NotFound(this ApiResponse<IEnumerable<Cryptocurrency>> apiResponse, int rank)
        {
            apiResponse.RequestError = "Rank: {Rank} not found, please use 1 <= rank <= 100";
            apiResponse.RequestStatusCode = 404;
            apiResponse.DbResult = new DbResult<IEnumerable<Cryptocurrency>>() { Data = Enumerable.Empty<Cryptocurrency>() };
            return apiResponse;
        }

        public static ApiResponse<IEnumerable<Cryptocurrency>> Success(this ApiResponse<IEnumerable<Cryptocurrency>> apiResponse, IEnumerable<Cryptocurrency> data)
        {
            apiResponse.RequestStatusCode = 200;
            apiResponse.DbResult = new DbResult<IEnumerable<Cryptocurrency>> { Data = data };
            apiResponse.IsValid = true;
            return apiResponse;
        }
    }
}
