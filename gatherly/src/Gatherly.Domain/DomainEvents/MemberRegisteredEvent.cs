using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.DomainEvents;

public record MemberRegisteredEvent(Guid MemberId) : IDomainEvent
{
}
