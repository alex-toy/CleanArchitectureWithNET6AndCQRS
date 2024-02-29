namespace Gatherly.Domain.Entities;

public class Invitation
{
    public Guid Id { get; private set; }

    public Guid GatheringId { get; private set; }

    public Guid MemberId { get; private set; }

    public InvitationStatus Status { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    internal Invitation(Guid id, Member member, Gathering gathering)
    {
        Id = id;
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.UtcNow;
    }
}