using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Base.Template.Domain.Common.Models;

[ExcludeFromCodeCoverage]
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }

    public DateTime CreatedDateTime { get; protected set; }

    //public string CreatedBy { get; set; }

    public DateTime UpdatedDateTime { get; protected set; }

    //public string UpdatedBy { get; set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? obj)
    {
        return Equals((object?)obj);
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }
}
