namespace Gatherly.Domain.Entities;

public class Gathering
{
    public Guid Id { get; private set; }

    public Member Creator { get; private set; }

    public GatheringType Type { get; private set; }

    public string Name { get; private set; }

    public DateTime ScheduledAtUtc { get; private set; }

    public string? Location { get; private set; }

    public int? MaximumNumberOfAttendees { get; private set; }

    public DateTime? InvitationsExpireAtUtc { get; private set; }

    public int NumberOfAttendees { get; set; }

    public List<Attendee> Attendees { get; set; }

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    private readonly List<Invitation> _invitations = new();

    private Gathering(Guid id, Member creator, GatheringType type, DateTime scheduledAtUtc, string? location, string name)
    {
        Id = id;
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Location = location;
        Name = name;
    }

    public static Gathering Create(Guid id, Member member, GatheringType type, DateTime scheduledAtUtc, string? location, string name, int? maxAttendeeCount, int? invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(Guid.NewGuid(), member, type, scheduledAtUtc, location, name);

        switch (gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maxAttendeeCount is null)
                {
                    throw new Exception($"{nameof(maxAttendeeCount)} can't be null.");
                }

                gathering.MaximumNumberOfAttendees = maxAttendeeCount;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new Exception($"{nameof(invitationsValidBeforeInHours)} can't be null.");
                }

                gathering.InvitationsExpireAtUtc = gathering.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }

        return gathering;
    }

    public Invitation SendInvitation(Member member)
    {
        if (Creator.Id == member.Id)
        {
            throw new Exception("Can't send invitation to the gathering creator.");
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            throw new Exception("Can't send invitation for gathering in the past.");
        }

        var invitation = new Invitation(Guid.NewGuid(), member, this);

        _invitations.Add(invitation);

        return invitation;
    }
}