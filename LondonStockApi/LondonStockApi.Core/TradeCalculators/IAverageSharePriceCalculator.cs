using LondonStockApi.Data.Models;

namespace LondonStockApi.Core.TradeCalculators
{
    public interface IAverageSharePriceCalculator
    {
        decimal SharePrice(List<Trade> trades);
    }
}