using System.ComponentModel.DataAnnotations;
using MassTransit;
using MassTransitDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class SendNotificationController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<bool> Notify(
        [StringLength(256, MinimumLength = 1)] string text,
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
