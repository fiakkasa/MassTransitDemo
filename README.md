# MassTransit Demo

Quick demo of using MassTransit!

📝 An event can be triggered via `SendNotificationController`.

## Spinning up the service

- Set your preferences
  ```json
  {
    "MassTransitConfig": {
      "UseOutbox": true,
      // to disable retries set one or all the Retry properties to null
      "RetryAttempts": 3,
      "RetryInterval": "00:00:02"
    },
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*"
  }
  ```
- VS Code: use the included profile
- cli: `dotnet run --project ./MassTransitDemo/MassTransitDemo.csproj --urls https://localhost:7382`

## Try it out!

- Swagger: https://localhost:7382/swagger

## References

- MassTransit: https://masstransit.io/documentation/concepts
