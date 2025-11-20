using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchasesController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] PurchaseRequest req)
    {
        return Ok(new PurchaseCreated(Guid.NewGuid().ToString("N")));
    }

    [HttpPost("cancel")]
    public IActionResult Cancel([FromBody] CancelRequest req)
    {
        return Ok();
    }
}

public sealed record PurchaseRequest(string ProductId, int Quantity);
public sealed record CancelRequest(string RequestId);
public sealed record PurchaseCreated(string RequestId);