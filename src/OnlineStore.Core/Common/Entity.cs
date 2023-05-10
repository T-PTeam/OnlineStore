using FluentValidation;
using FluentValidation.Results;
using OnlineStore.Core.Exceptions;

namespace OnlineStore.Core.Common;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Domain events occurred.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected static void Validate<T>(AbstractValidator<T> validator, T data)
    {
        var validationResult = validator.Validate(data);
        ThrowIfNotValid(validationResult);
    }

    protected static async Task ValidateAsync<T>(AbstractValidator<T> validator, T data,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(data, cancellationToken);
        ThrowIfNotValid(validationResult);
    }

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="domainEvent"></param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        var ruleResult = rule.Check();
        if (ruleResult.IsFailed)
        {
            throw new RuleValidationException(ruleResult.Errors);
        }
    }

    /// <summary>
    /// Clear domain events.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    private static void ThrowIfNotValid(ValidationResult validationResult)
    {
        // todo: think to implement extension method IsInValid
        if (!validationResult.IsValid) throw new Exceptions.ValidationException(validationResult.Errors);
    }
}