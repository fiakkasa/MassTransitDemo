using MassTransit;
using MassTransitDemo.Models;

namespace MassTransitDemo.Services;

public class MessageConsumerService(ILogger<MessageConsumerService> logger) : IConsumer<Message>
{
    public async Task Consume(ConsumeContext<Message> context)
    {
        var text = context.Message.Text;

        var textSample = text switch
        {
            { Length: > 10 } => text[0..9] + "..",
            _ => text
        };

        logger.LogInformation("Starting to process text '{TextSample}'!", textSample);

         // note: simulate an exception
        if (text == "crash") throw new InvalidOperationException("Splash!");

        // note: simulate work
        await Task.Delay(TimeSpan.FromSeconds(15), context.CancellationToken);

        logger.LogInformation("Finished processing text '{TextSample}'!", textSample);
    }
}
