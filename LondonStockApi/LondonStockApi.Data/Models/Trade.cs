using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockApi.Data.Models
{
    public class Trade
    {
        public int StockId { get; set; }
        public decimal Price { get; set; }
        public decimal Shares { get; set; }
        public int BrokerID { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
