using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.DomainEvents;

public sealed record InvitationAcceptedEvent(Guid InvitationId, Guid GatheringId) : IDomainEvent
{
}
