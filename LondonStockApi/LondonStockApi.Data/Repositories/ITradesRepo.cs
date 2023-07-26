using LondonStockApi.Data.Models;

namespace LondonStockApi.Data.Repositories
{
    public interface ITradesRepo
    {
        Task AddTrade(Trade trade);
        Task<List<Trade>> GetTrades(int stockId);
        Task<List<Trade>> GetTrades(List<int> stockIds);
    }
}