using LondonStockApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockApi.Data.Repositories
{
    public class FakeStocksRepo : IStocksRepo
    {
        List<Stock> _stocks;
        public FakeStocksRepo()
        {
            _stocks = new List<Stock> {
                new Stock { Id = 1, StockTicker = "VOD" },
                new Stock { Id = 2, StockTicker = "GLEN" },
                new Stock { Id = 3, StockTicker = "RR." },
                new Stock { Id = 4, StockTicker = "CNA" },
                new Stock { Id = 5, StockTicker = "BP." }
            };
        }

        public async Task<Stock> GetStock(string stockTicker)
        {
            return _stocks.FirstOrDefault(stock => stock.StockTicker == stockTicker);
        }

        public async Task<List<Stock>> GetStocks(List<string> stockTickers)
        {
            return _stocks.Where(stock => stockTickers.Contains(stock.StockTicker)).ToList();
        }

        public async Task<List<Stock>> GetStocks()
        {
            return _stocks.ToList();
        }
    }
}
