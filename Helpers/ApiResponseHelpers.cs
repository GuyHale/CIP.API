using CIP.API.Interfaces;
using CIP.API.Models;
using Microsoft.AspNetCore.Identity;
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

        public static ICustomResponse FailureResponse<T>(IEnumerable<IdentityError> identityErrors) where T : ICustomResponse, new()
        {
            T registrationResponse = new()
            {
                Success = false,
                ErrorMessages = identityErrors.Select(x => x.Description)
            };

            return registrationResponse;
        }

        public static ICustomResponse SuccessResponse<T>() where T : ICustomResponse, new()
        {
            return
            new T()
            {
                Success = true
            };
        }

        public static ICustomResponse LoginFailure<T>() where T : ICustomResponse, new()
        {
            return new T()
            {
                Success = false
            };
        }

        public static ICustomResponse ServerError<T>() where T : ICustomResponse, new()
        {
            return new T() { Success = false, ErrorMessages = new string[] { "Sorry something unexpected has happened, please try again" } };
        }
    }
}
