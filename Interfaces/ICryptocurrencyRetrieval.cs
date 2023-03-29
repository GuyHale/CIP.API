using CIP.API.Models;

namespace CIP.API.Interfaces
{
    public interface ICryptocurrencyRetrieval
    {
        Task<IEnumerable<Cryptocurrency>> Get();
    }
}
