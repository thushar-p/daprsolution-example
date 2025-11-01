using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace DaprSolution.OrderService.Http.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        DaprClient client { get; set; }
        public OrderController(DaprClient daprClient) 
        {
            client = daprClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionList(DaprClient client)
        {
            Console.WriteLine("Get Transaction List from Transaction Service");
            var response = await client.CreateInvokableHttpClient(appId: "transaction-service").GetFromJsonAsync<List<string>>("transaction");
            Console.WriteLine(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionCode transactionCode)
        {
            Console.WriteLine(transactionCode.Code);
            var response = await client.CreateInvokableHttpClient(appId: "transaction-service").PostAsJsonAsync("transaction", transactionCode);
            return Ok(response);
        }

        [HttpPost]
        [Route("publish")]
        public async Task<IActionResult> Publisher([FromBody] TransactionCode transactionCode)
        {
            Console.WriteLine(transactionCode.Code);
            await client.PublishEventAsync("orderpub", "orderTopic", transactionCode);
            return Ok();
        }


        [HttpPost]
        [Route("publish-bill-break")]
        public async Task<IActionResult> billBreakPub([FromBody] TransactionCode transactionCode)
        {
            Console.WriteLine(transactionCode.Code);
            await client.PublishEventAsync("billBreakPub", "billBreakTopic", transactionCode);
            return Ok();
        }
    }

    public class TransactionCode
    {
        public string Code { get; set; }
    }
}
