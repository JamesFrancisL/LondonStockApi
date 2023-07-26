using System.IO;
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
    public class UploadTradeEndpoint
    {
        private readonly IAddTradeService _addTradeService;
        private readonly ILogger<UploadTradeEndpoint> _logger;

        public UploadTradeEndpoint(IAddTradeService addTradeService, ILogger<UploadTradeEndpoint> log)
        {
            _addTradeService = addTradeService;
            _logger = log;
        }

        [FunctionName("UploadTrade")]
        [OpenApiOperation(operationId: "UploadTrade")]
        [OpenApiRequestBody("application/json", typeof(TradeApiModel), Description = "The trade to be added")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(AddTradeResponse), Description = "The OK response when trade added")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(AddTradeResponse), Description = "The Bad Request response when trade add failed")]
        public async Task<IActionResult> UploadTrade(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            //ToDo improved message validation and response
            if (requestBody.Length == 0)
                new BadRequestObjectResult(new AddTradeResponse { Success = false, Message = "Trade required in message body"});
            var tradeAddSuccess = await _addTradeService.AddTrade(requestBody);

            return tradeAddSuccess.Success ? new OkObjectResult(tradeAddSuccess) : new BadRequestObjectResult(tradeAddSuccess);
        }
    }
}

