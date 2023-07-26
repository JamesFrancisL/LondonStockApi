using LondonStockApi.Functions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockApi.Functions.Services
{
    public interface IStockPriceLookupService
    {
        Task<List<StockPrice>> StockPrice(List<string> stockTicker);
    }
}