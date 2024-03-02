using System.Runtime.ExceptionServices;

namespace Gatherly.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; private init; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public bool Equals(Entity? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        return obj.Id == Id;
    }

    public static bool operator == (Entity? left, Entity? right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator != (Entity? left, Entity? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        if (obj is not Entity entity) return false;

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}
