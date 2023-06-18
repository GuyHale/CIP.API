﻿using CIP.API.Helpers;
using CIP.API.Interfaces;
using CIP.API.Models;
using CIP.API.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Controllers
{
    [Authorize]
    [Route("cip/api")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ICryptocurrencyApi _cryptocurrencyApi;
        private readonly ILogger<DeveloperController> _logger;

        public DeveloperController(ICryptocurrencyApi cryptocurrencyApi, ILogger<DeveloperController> logger)
        {
            _cryptocurrencyApi = cryptocurrencyApi;
            _logger = logger;
        }

        [Route("{apiKey}/get/all")]
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<Cryptocurrency>>> Get(string apiKey)
        {
            try
            {
                return await _cryptocurrencyApi.GetAll(apiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.InternalError();
        }
        
        [Route("{apiKey}/get/{rank}")]
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<Cryptocurrency>>> Get(string apiKey, int rank)
        {
            try
            {
                return await _cryptocurrencyApi.GetAll(apiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.InternalError();
        }
    }
}
