using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : AggregateRoot
{
    public Member(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private Member()
    {
    }

    public FirstName FirstName { get; set; }

    public LastName LastName { get; set; }

    public Email Email { get; set; }

    public static Member Create(Guid guid, FirstName? firstName, LastName? lastName, Email? email)
    {
        var member = new Member(guid, firstName, lastName, email)
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        member.RaiseDomainEvent(new MemberRegisteredEvent(member.Id));

        return member;
    }
}