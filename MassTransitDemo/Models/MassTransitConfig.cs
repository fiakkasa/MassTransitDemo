using System.ComponentModel.DataAnnotations;

namespace MassTransitDemo.Models;

public record MassTransitConfig : IValidatableObject
{
    public bool UseOutbox { get; init; } = true;

    [Range(1, 16)]
    public int? RetryAttempts { get; init; }

    public TimeSpan? RetryInterval { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!(RetryInterval is null or { TotalMilliseconds: > 0, TotalHours: <= 1 }))
            yield return new("Value must represent a positive time frame between 1 ms and 1 hour", [nameof(RetryInterval)]);
    }
}
