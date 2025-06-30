namespace MyMuscles.Domain.Shared;

public abstract class ValueObjectBase : Notificavel, IEquatable<ValueObjectBase>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObjectBase? other)
    {
        if (other is null) return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj) =>
        obj is ValueObjectBase other && Equals(other);

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                return HashCode.Combine(current, obj != null ? obj.GetHashCode() : 0);
            });
    }

    public static bool operator ==(ValueObjectBase a, ValueObjectBase b) => a is null && b is null || a?.Equals(b) == true;

    public static bool operator !=(ValueObjectBase a, ValueObjectBase b) => !(a == b);
}
