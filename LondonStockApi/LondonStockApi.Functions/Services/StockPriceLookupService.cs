using LondonStockApi.Core.TradeCalculators;
using LondonStockApi.Data.Repositories;
using LondonStockApi.Functions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockApi.Functions.Services
{
    public class StockPriceLookupService : IStockPriceLookupService
    {
        private readonly ITradesRepo _tradesRepo;
        private readonly IStocksRepo _stocksRepo;
        private readonly IAverageSharePriceCalculator _averageSharePriceCalculator;

        public StockPriceLookupService(ITradesRepo tradesRepo, IStocksRepo stocksRepo, IAverageSharePriceCalculator averageSharePriceCalculator)
        {
            _tradesRepo = tradesRepo;
            _stocksRepo = stocksRepo;
            _averageSharePriceCalculator = averageSharePriceCalculator;
        }

        public async Task<List<StockPrice>> StockPrice(List<string> stockTicker)
        {
            //ToDo these could be combined into one query but it should also handle larger requests where 
            // the number of items requested may not be retrieved from DB all together, in which case 
            // seperate queries might still be the way to go.
            var stocks = stockTicker.Count == 0 ? await _stocksRepo.GetStocks()
                : await _stocksRepo.GetStocks(stockTicker);
            var stockTrades =  await _tradesRepo.GetTrades(stocks.Select(stock => stock.Id).ToList());

            var stockPrices = stocks.Select(stockT =>
                    {
                        return new StockPrice
                        {
                            StockTicker = stockT.StockTicker,
                            Price = _averageSharePriceCalculator.SharePrice(stockTrades.Where(trade => trade.StockId == stockT.Id).ToList())
                        };
                    }
                    );

            return stockPrices.ToList();
        }
    }
}
