using LondonStockApi.Data.Models;

namespace LondonStockApi.Data.Repositories
{
    public interface IStocksRepo
    {
        Task<Stock> GetStock(string stockTicker);
        Task<List<Stock>> GetStocks(List<string> stockTickers);
        Task<List<Stock>> GetStocks();
    }
}