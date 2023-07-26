using FluentAssertions;
using LondonStockApi.Core.TradeCalculators;
using LondonStockApi.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockApi.Core.Tests.TradeCalculators
{
    public class AverageSharePriceCalculatorTests
    {
        private readonly AverageSharePriceCalculator _averageSharePriceCalculator;
        private readonly Mock<ITradePriceCalculator> _tradePriceCaclulator = new Mock<ITradePriceCalculator>();

        public AverageSharePriceCalculatorTests()
        {
            _averageSharePriceCalculator = new AverageSharePriceCalculator(_tradePriceCaclulator.Object);
        }

        public void SharePrice_CalculatesAverageSharePrice()
        {
            //arrange
            var listTrades = new List<Trade> {
                    new Trade
                    {
                        StockId = 1
                        , Price = 100
                        , Shares = 10
                        , BrokerID = 18
                    },
                    new Trade
                    {
                        StockId = 1
                        , Price = 200
                        , Shares = 20
                        , BrokerID = 55
                    },
                    new Trade
                    {
                        StockId = 1
                        , Price = 40
                        , Shares = 1
                        , BrokerID = 12
                    }
            };

            //act
            var resultAveragePrices = _averageSharePriceCalculator.SharePrice(listTrades);

            //assert
            resultAveragePrices.Should().Be(20);
        }
    }
}
