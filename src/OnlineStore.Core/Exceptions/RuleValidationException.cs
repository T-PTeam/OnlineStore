namespace OnlineStore.Core.Exceptions;

public class RuleValidationException : DomainException
{
    public RuleValidationException(IEnumerable<string> failures) : base("Validation is failed.")
    {
        Failures = failures.ToList().AsReadOnly();
    }

    public IReadOnlyCollection<string> Failures { get; }
}