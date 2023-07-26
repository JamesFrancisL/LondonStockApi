using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LondonStockApi.Functions.Models;
using LondonStockApi.Functions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace LondonStockApi.Functions.RestEndPoints
{
    public class StockPriceLookupEndpoint
    {
        private readonly IStockPriceLookupService _stockPriceLookupService;
        private readonly ILogger<StockPriceLookupEndpoint> _logger;

        public StockPriceLookupEndpoint(IStockPriceLookupService stockPriceLookupService, ILogger<StockPriceLookupEndpoint> log)
        {
            _stockPriceLookupService = stockPriceLookupService;
            _logger = log;
        }

        [FunctionName("PriceLookup")]
        [OpenApiOperation(operationId: "PriceLookup")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "stockticker", In = ParameterLocation.Query, Required = false, Type = typeof(List<string>), Description = "The stock ticker parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(List<StockPrice>), Description = "The OK response if stock price lookup successful")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation($"Function PriceLookup processed a request.");

            //ToDo API query string read and validation
            var stockTicker = req.Query["stockticker"].ToString()
                .Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            var responseMessage = await _stockPriceLookupService.StockPrice(stockTicker);

            //ToDo handling of failed lookup and return object for this
            return new OkObjectResult(responseMessage);
        }
    }
}

