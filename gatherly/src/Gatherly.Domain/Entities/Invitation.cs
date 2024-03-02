﻿using Gatherly.Domain.Enums;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities;

public sealed class Invitation : Entity
{
    public Guid GatheringId { get; private set; }

    public Guid MemberId { get; private set; }

    public InvitationStatus Status { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    internal Invitation(Guid id, Member member, Gathering gathering) : base(id)
    {
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.UtcNow;
    }

    internal void Expire()
    {
        Status = InvitationStatus.Expired;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    internal Attendee Accept()
    {
        Status = InvitationStatus.Accepted;
        ModifiedOnUtc = DateTime.UtcNow;

        var attendee = new Attendee(this);

        return attendee;
    }
}