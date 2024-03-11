using GreenPipes;
using MassTransit;
using MassTransitDemo.Models;
using Microsoft.Extensions.Options;

namespace MassTransitDemo.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitServices(this IServiceCollection services)
    {
        services
            .AddOptions<MassTransitConfig>()
            .BindConfiguration(nameof(MassTransitConfig))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMassTransit(builder =>
        {
            builder.SetKebabCaseEndpointNameFormatter();

            // automatically add consumers from a specified assembly
            builder.AddConsumers(typeof(Program).Assembly);

            // using the in memory provider for the demo; other providers can be added as well
            builder.UsingInMemory((context, options) =>
            {
                var massTransitConfig = context.GetRequiredService<IOptionsMonitor<MassTransitConfig>>().CurrentValue;

                if (massTransitConfig.UseOutbox)
                    options.UseInMemoryOutbox();

                if(massTransitConfig is { RetryAttempts: { } retryAttempts, RetryInterval: { } retryInterval })
                    options.UseMessageRetry(retryOptions => retryOptions.Interval(retryAttempts, retryInterval));

                options.ConfigureEndpoints(context);
            });

        });

        services.AddMassTransitHostedService();

        return services;
    }
}
