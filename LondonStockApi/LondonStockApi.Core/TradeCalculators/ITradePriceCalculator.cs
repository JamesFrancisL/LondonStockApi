using LondonStockApi.Data.Models;

namespace LondonStockApi.Core.TradeCalculators
{
    public interface ITradePriceCalculator
    {
        decimal SharePrice(Trade trade);
    }
}