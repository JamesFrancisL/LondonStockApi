using LondonStockApi.Data.Models;

namespace LondonStockApi.Data.Repositories
{
    public class FakeTradesRepo : ITradesRepo
    {
        public List<Trade> _trades = new List<Trade>();
        public async Task AddTrade(Trade trade)
        {
            _trades.Add(trade);
        }

        public async Task<List<Trade>> GetTrades(int stockId)
        {
            return _trades.Where(trade => trade.StockId == stockId).ToList();
        }

        public async Task<List<Trade>> GetTrades(List<int> stockIds)
        {
            return _trades.Where(trade => stockIds.Contains(trade.StockId)).ToList();
        }

        public async Task<List<Trade>> GetTrades()
        {
            return _trades.ToList();
        }
    }
}
