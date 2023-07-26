using LondonStockApi.Core.TradeCalculators;
using LondonStockApi.Data.Repositories;
using LondonStockApi.Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]
namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            // optional: customize your configuration sources 
            // here, we add appsettings.json files 
            // Note that these files are not automatically copied on build or publish. 
            //builder.ConfigurationBuilder
            //    .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
            //    .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false);
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            // get the configuration from the builder
            var configuration = builder.GetContext().Configuration;
            var services = builder.Services;

            services.AddSingleton<ITradesRepo, FakeTradesRepo>();
            services.AddSingleton<IStocksRepo, FakeStocksRepo>();
            services.AddTransient<IAddTradeService, AddTradeService>();
            services.AddTransient<IStockPriceLookupService, StockPriceLookupService>();
            services.AddTransient<ITradePriceCalculator, TradePriceCalculator>();
            services.AddTransient<IAverageSharePriceCalculator, AverageSharePriceCalculator>();
        }
    }
}