using System;
using System.Collections.Generic;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : IEquatable<TId>
{
    public TId Id { get; protected set; }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (IsTransient() || other.IsTransient())
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> otherEntity)
        {
            return false;
        }

        return Equals(otherEntity);
    }

    public override int GetHashCode()
    {
        // NOTE: Id가 아직 설정되지 않은 경우 (트랜지언트) 해시코드가 변경될 수 있으므로 주의
        // NOTE: 지금은 Id가 있으면 Id의 해시코드를, 없으면 기본 Object 해시코드를 반환
        return IsTransient() ? base.GetHashCode() : Id.GetHashCode();
    }

    protected bool IsTransient()
    {
        // NOTE: TId가 IEquatable<TId>를 구현한다고 가정
        return EqualityComparer<TId>.Default.Equals(Id, default(TId));
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null);
        }
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }
}