using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockApi.Functions.Models
{
    public class TradeApiModel
    {
        public string StockTicker { get; set; }
        public decimal Price { get; set; }
        public decimal Shares { get; set; }
        public int BrokerID { get; set; }
    }
}
