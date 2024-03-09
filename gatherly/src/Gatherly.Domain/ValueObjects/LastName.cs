using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public class LastName : ValueObject
{
    public const int MaxLength = 20;
    public string Value { get; }

    private LastName(string value)
    {
        Value = value;
    }

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<LastName>(new Error("LastName.Empty", "LastName is empty"));
        if (value.Length > MaxLength) return Result.Failure<LastName>(new Error("LastName.TooLong", "LastName is too long"));
        return new LastName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
