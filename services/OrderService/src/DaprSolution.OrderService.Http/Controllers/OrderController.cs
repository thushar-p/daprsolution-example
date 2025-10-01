using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DaprSolution.OrderService.Http.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        HttpClient client;
        public OrderController() 
        {
            client = DaprClient.CreateInvokeHttpClient(appId: "transaction");
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionList()
        {
            Console.WriteLine("Get Transaction List from Transaction Service");
            var response = await client.GetFromJsonAsync<List<string>>("transaction");
            Console.WriteLine(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionCode transactionCode)
        {
            Console.WriteLine(transactionCode.Code);
            var response = await client.PostAsJsonAsync("transaction", transactionCode);
            return Ok(response);
        }
    }

    public class TransactionCode
    {
        public string Code { get; set; }
    }
}
