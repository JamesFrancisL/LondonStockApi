using LondonStockApi.Functions.Models;
using System.Threading.Tasks;

namespace LondonStockApi.Functions.Services
{
    public interface IAddTradeService
    {
        Task<AddTradeResponse> AddTrade(string tradeMessage);
    }
}