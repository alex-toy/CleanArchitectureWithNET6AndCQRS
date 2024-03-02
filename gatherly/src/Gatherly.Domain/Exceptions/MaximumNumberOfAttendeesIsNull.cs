namespace Gatherly.Domain.Exceptions;

public sealed class MaximumNumberOfAttendeesIsNull : DomainException
{
    public MaximumNumberOfAttendeesIsNull(string message) : base(message) { }
}
