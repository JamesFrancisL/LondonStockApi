using LondonStockApi.Data.Models;
using LondonStockApi.Data.Repositories;
using LondonStockApi.Functions.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LondonStockApi.Functions.Services
{
    public class AddTradeService : IAddTradeService
    {
        private readonly ITradesRepo _tradesRepo;
        private readonly IStocksRepo _stocksRepo;

        public AddTradeService(ITradesRepo tradesRepo, IStocksRepo stocksRepo)
        {
            _tradesRepo = tradesRepo;
            _stocksRepo = stocksRepo;
        }
        public async Task<AddTradeResponse> AddTrade(string tradeMessage)
        {
            try
            {
                var trade = JsonConvert.DeserializeObject<TradeApiModel>(tradeMessage);
                var stock = await _stocksRepo.GetStock(trade.StockTicker);

                if (stock == null) return new AddTradeResponse { Success = false, Message = $"Trade add failed - stock {trade.StockTicker} doesn't exist" };

                await _tradesRepo.AddTrade(new Trade { StockId = stock.Id, Shares = trade.Shares, Price = trade.Price, BrokerID = trade.BrokerID });
                return new AddTradeResponse { Success = true, Message = "Trade successfully added"};
            }
            catch (Exception)
            {
                return new AddTradeResponse { Success = false, Message = "Trade add failed" };
            }
        }
    }
}
