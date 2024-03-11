using MassTransit;
using MassTransitDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class SendNotificationController : ControllerBase
{
    [HttpGet]
    public async Task<bool> Notify(
        string text,
        [FromServices] IBus bus,
        [FromServices] ILogger<SendNotificationController> logger,
        CancellationToken cancellationToken = default
    )
    {
        logger.LogInformation("Notification Request Arrived!");

        await bus.Publish(new Message(text), cancellationToken);

        logger.LogInformation("Notification Request Sent!");

        return true;
    }
}
