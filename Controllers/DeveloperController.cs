﻿using CIP.API.Helpers;
using CIP.API.Identity;
using CIP.API.Interfaces;
using CIP.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CIP.API.Controllers
{
    [Authorize]
    [Route("cip/api")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ICryptocurrencyRetrieval _cryptocurrencyRetrieval;
        private readonly ICustomAuthenticationService _customAuthenticationService;
        private readonly ILogger<DeveloperController> _logger;

        public DeveloperController(ICryptocurrencyRetrieval cryptocurrencyRetrieval, ICustomAuthenticationService customAuthenticationService, ILogger<DeveloperController> logger)
        {
            _cryptocurrencyRetrieval = cryptocurrencyRetrieval;
            _customAuthenticationService = customAuthenticationService;
            _logger = logger;
        }

        [Route("{apiKey}/get/all")]
        [HttpGet]
        public async Task<ApiResponse> Get(string apiKey)
        {
            ApiResponse apiResponse = new();
            try
            {
                return await RequestResponse(apiResponse, apiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return apiResponse.InternalError();
        }

        [Route("{apiKey}/get/{name}")]
        [HttpGet]
        public async Task<ApiResponse> Get(string apiKey, string name)
        {
            ApiResponse apiResponse = new();
            try
            {
                return await RequestResponse(apiResponse, apiKey, name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return apiResponse.InternalError();
        }
        
        [Route("{apiKey}/get/{rank}")]
        [HttpGet]
        public async Task<ApiResponse> Get(string apiKey, int rank)
        {
            ApiResponse apiResponse = new();
            try
            {
                return await RequestResponse(apiResponse, apiKey, rank);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return apiResponse.InternalError();
        }

        private async Task<bool> ApiKeyValidation(string apiKey)
        {
            return string.IsNullOrEmpty(await _customAuthenticationService.GetApiKey(apiKey));
        }

        private async Task<ApiResponse> RequestResponse(ApiResponse apiResponse, string apiKey)
        {
            if (!await ApiKeyValidation(apiKey))
            {
                return apiResponse.InvalidApiKey();
            }
            IEnumerable<Cryptocurrency> cryptocurrencies = await _cryptocurrencyRetrieval.Get();
            if (!cryptocurrencies.Any())
            {
                return apiResponse.InternalError();
            }
            return apiResponse.Success(cryptocurrencies);
        }
        
        private async Task<ApiResponse> RequestResponse(ApiResponse apiResponse, string apiKey, string name)
        {
            if (!await ApiKeyValidation(apiKey))
            {
                return apiResponse.InvalidApiKey();
            }
            Cryptocurrency cryptocurrency = await _cryptocurrencyRetrieval.Get(name);
            if (cryptocurrency is null)
            {
                return apiResponse.NotFound(name);
            }
            return apiResponse.Success(new List<Cryptocurrency>() { cryptocurrency });
        }
        
        private async Task<ApiResponse> RequestResponse(ApiResponse apiResponse, string apiKey, int rank)
        {
            if (!await ApiKeyValidation(apiKey))
            {
                return apiResponse.InvalidApiKey();
            }
            Cryptocurrency cryptocurrency = await _cryptocurrencyRetrieval.Get(rank);
            if (cryptocurrency is null)
            {
                return apiResponse.InternalError();
            }
            return apiResponse.Success(new List<Cryptocurrency>() { cryptocurrency });
        }
    }
}
