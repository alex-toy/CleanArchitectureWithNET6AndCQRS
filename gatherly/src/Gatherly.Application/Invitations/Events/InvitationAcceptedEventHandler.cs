using Gatherly.Application.Abstractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Invitations.Events;

public class InvitationAcceptedEventHandler : INotificationHandler<InvitationAcceptedEvent>
{
    private readonly IEmailService _emailService;
    private readonly IGatheringRepository _gatheringRepository;

    public async Task Handle(InvitationAcceptedEvent notification, CancellationToken cancellationToken)
    {
        Gathering? gathering = await _gatheringRepository.GetByIdWithCreatorAsync(notification.GatheringId, cancellationToken);
        if (gathering is null) return;

        await _emailService.SendInvitationAcceptedEmailAsync(gathering, cancellationToken);
    }
}
