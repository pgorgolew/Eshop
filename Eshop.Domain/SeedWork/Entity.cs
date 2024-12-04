using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Domain.SeedWork;

/// <summary>
/// Base class for entities.
/// </summary>
public abstract class Entity(Guid id)
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; } = id;

    // No readonly modifier, allowing initialization during deserialization
    private List<IDomainEvent>? _domainEvents;

    /// <summary>
    /// Domain events occurred.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => (_domainEvents = _domainEvents ?? []).AsReadOnly();

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="domainEvent"></param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        (_domainEvents = _domainEvents ?? []).Add(domainEvent);
    }

    /// <summary>
    /// Clear domain events.
    /// </summary>
    public void ClearDomainEvents()
    {
        (_domainEvents = _domainEvents ?? []).Clear();
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}