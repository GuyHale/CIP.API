﻿using CIP.API.Interfaces;
using CIP.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIP.API.Controllers
{
    [Authorize]
    [Route("cip/api")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ICryptocurrencyRetrieval _cryptocurrencyRetrieval;
        private readonly ILogger<DeveloperController> _logger;

        public DeveloperController(ICryptocurrencyRetrieval cryptocurrencyRetrieval, ILogger<DeveloperController> logger)
        {
            _cryptocurrencyRetrieval = cryptocurrencyRetrieval;
            _logger = logger;
        }

        [Route("get")]
        [HttpGet]
        public async Task<IEnumerable<Cryptocurrency>> Get()
        {
            try
            {
                return await _cryptocurrencyRetrieval.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Enumerable.Empty<Cryptocurrency>();
        }
    }
}
