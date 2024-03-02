using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 20;
    public string Value { get; }

    private FirstName(string value)
    {
        Value = value;
    }

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<FirstName>(new Error("FirstName.Empty", "FirstName is empty"));
        if (value.Length > MaxLength) return Result.Failure<FirstName>(new Error("FirstName.TooLong", "FirstName is too long"));
        return new FirstName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
