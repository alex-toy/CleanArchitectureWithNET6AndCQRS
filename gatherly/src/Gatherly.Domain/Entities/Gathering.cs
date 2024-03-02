using Gatherly.Domain.Enums;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Exceptions;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Entities;

public sealed class Gathering : Entity
{
    public Member Creator { get; private set; }

    public GatheringType Type { get; private set; }

    public string Name { get; private set; }

    public DateTime ScheduledAtUtc { get; private set; }

    public string? Location { get; private set; }

    public int? MaximumNumberOfAttendees { get; private set; }

    public DateTime? InvitationsExpireAtUtc { get; private set; }

    public int NumberOfAttendees { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees => _attendee;

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendee = new();

    private Gathering(Guid id, Member creator, GatheringType type, DateTime scheduledAtUtc, string? location, string name) : base(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Location = location;
        Name = name;
    }

    public static Gathering Create(Guid id, Member member, GatheringType type, DateTime scheduledAtUtc, string? location, string name, int? maxAttendeeCount, int? invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(Guid.NewGuid(), member, type, scheduledAtUtc, location, name);

        gathering.CalculateTypeDetails(maxAttendeeCount, invitationsValidBeforeInHours);

        return gathering;
    }

    private void CalculateTypeDetails(int? maxAttendeeCount, int? invitationsValidBeforeInHours)
    {
        switch (Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maxAttendeeCount is null) throw new MaximumNumberOfAttendeesIsNull($"{nameof(maxAttendeeCount)} can't be null.");
                MaximumNumberOfAttendees = maxAttendeeCount;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null) throw new ValidBeforeInHourIsNull($"{nameof(invitationsValidBeforeInHours)} can't be null.");
                InvitationsExpireAtUtc = ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }
    }

    public Result<Invitation> SendInvitation(Member member)
    {
        if (Creator.Id == member.Id) return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);
        
        if (ScheduledAtUtc < DateTime.UtcNow) return Result.Failure<Invitation>(DomainErrors.Gathering.AlreadyPassed);

        var invitation = new Invitation(Guid.NewGuid(), member, this);

        _invitations.Add(invitation);

        return invitation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {

        bool expired = (Type == GatheringType.WithFixedNumberOfAttendees && NumberOfAttendees < MaximumNumberOfAttendees) ||
                       (Type == GatheringType.WithExpirationForInvitations && InvitationsExpireAtUtc < DateTime.UtcNow);

        if (expired)
        {
            invitation.Expire();
            return null;
        }

        Attendee attendee = invitation.Accept();
        _attendee.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
}