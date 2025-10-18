using Microsoft.AspNetCore.Mvc;

namespace DaprSolution.TransactionService.Http.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;

        private static List<string> TransactionCode = new List<string>
        {
            "T001", "T002"
        };

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("Get Transaction List");
            return Ok(TransactionCode);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TransactionCode transactionCode)
        {
            Console.WriteLine("Post Transaction");
            TransactionCode.Add(transactionCode.Code);
            return Ok(transactionCode);
        }

        [Dapr.Topic("orderpub", "orderTopic")]
        [Dapr.Topic("billBreakPub", "billBreakTopic")]
        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] TransactionCode transactionCode)
        {
            Console.WriteLine($"Subscriber: {transactionCode.Code}");
            TransactionCode.Add(transactionCode.Code);
            return Ok(transactionCode);
        }
    }

    public class TransactionCode
    {
        public string Code { get; set; }
    }

}
