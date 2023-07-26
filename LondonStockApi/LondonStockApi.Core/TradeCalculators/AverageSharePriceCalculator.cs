using LondonStockApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockApi.Core.TradeCalculators
{
    public class AverageSharePriceCalculator : IAverageSharePriceCalculator
    {
        private readonly ITradePriceCalculator _tradePriceCaclulator;

        public AverageSharePriceCalculator(ITradePriceCalculator tradePriceCaclulator)
        {
            _tradePriceCaclulator = tradePriceCaclulator;
        }
        public decimal SharePrice(List<Trade> trades)
        {
            //ToDo Should the below be 0, would clarify expected response with PO
            if (trades.Count == 0) return 0;
            //ToDo Should this figure be rounded to 2 decimal places or other precision?
            return trades.Sum(stockTrade => _tradePriceCaclulator.SharePrice(stockTrade)) / trades.Count;
        }
    }
}
