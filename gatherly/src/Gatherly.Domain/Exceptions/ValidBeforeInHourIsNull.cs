namespace Gatherly.Domain.Exceptions;

public sealed class ValidBeforeInHourIsNull : DomainException
{
    public ValidBeforeInHourIsNull(string message) : base(message) { }
}
