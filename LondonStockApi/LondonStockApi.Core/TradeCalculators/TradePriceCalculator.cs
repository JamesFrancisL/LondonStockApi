using LondonStockApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockApi.Core.TradeCalculators
{
    public class TradePriceCalculator : ITradePriceCalculator
    {
        public decimal SharePrice(Trade trade)
        {
            //ToDo Should the below be 0, would clarify expected response with PO
            if (trade.Shares == 0) return 0;
            //ToDo Should this figure be rounded to 2 decimal places or other precision?
            return trade.Price / trade.Shares;
        }
    }
}
