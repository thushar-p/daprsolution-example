using Microsoft.AspNetCore.Mvc;

namespace DaprSolution.PaymentService.Http.Controllers;

[ApiController]
[Route("payment")]
public class PaymentController : ControllerBase
{
    private static List<string> TransactionCode = new List<string>
    {
        "T001", "T002"
    };

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