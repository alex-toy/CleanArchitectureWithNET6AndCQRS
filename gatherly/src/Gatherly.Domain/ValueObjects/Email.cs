using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;
using System.Text.RegularExpressions;

namespace Gatherly.Domain.ValueObjects;

public class Email : ValueObject
{
    private static readonly Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<Email>(new Error("Email.Empty", "Email is empty"));

        Match match = regex.Match(value);
        if (!match.Success) return Result.Failure<Email>(new Error("Email.Format", "wrong Email format"));
        return new Email(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
