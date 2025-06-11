namespace SharedKernel.Abstractions;

public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>>
    where TId : notnull, IEquatable<TId>, IComparable<TId>
{
    public TId Id { get; protected set; } = default!;
    public override bool Equals(object? obj) => Equals(obj as BaseEntity<TId>);
    public bool Equals(BaseEntity<TId>? other) => other is not null && Id.Equals(other.Id);
    public override int GetHashCode() => Id.GetHashCode() * 41;
    public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right) => Equals(left, right);
    public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right) => !Equals(left, right);
}
