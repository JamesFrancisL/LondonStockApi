using FluentAssertions;
using LondonStockApi.Core.TradeCalculators;
using LondonStockApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LondonStockApi.Core.Tests.TradeCalculators
{
    public class TradePriceCalculatorTests
    {
        private TradePriceCalculator _tradePriceCalculator = new TradePriceCalculator();

        [Theory]
        [InlineData(200.00, 10, 20)]
        [InlineData(10, 0.5, 20)]
        [InlineData(188.88, 1, 188.88)]
        public async Task TradePriceCalculator_CalculatesTradePrices(decimal price, decimal shares, decimal expectedResultPrice)
        {
            //arrange
            var trade = new Trade
                    {
                        StockId = 1
                        , Price = price
                        , Shares = shares
                        , BrokerID = 18
                    };

            //act
            var resultPrice = _tradePriceCalculator.SharePrice(trade);

            //assert
            resultPrice.Should().Be(expectedResultPrice);
        }
    }
}
