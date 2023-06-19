using CIP.API.Models;
using CIP.API.Models.Responses;

namespace CIP.API.Interfaces
{
    public interface ICryptocurrencyApi
    {
        Task<ApiResponse<IEnumerable<Cryptocurrency>>> GetAll(string apiKey);
    }
}
