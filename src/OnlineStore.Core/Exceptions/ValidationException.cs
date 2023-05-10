using FluentValidation.Results;

namespace OnlineStore.Core.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(List<ValidationFailure> failures) : base("Validation is failed.")
    {
        Failures = failures.AsReadOnly();
    }

    public ValidationException(ValidationFailure failure) : base("Validation is failed.")
    {
        Failures = new[] { failure };
    }

    public IReadOnlyCollection<ValidationFailure> Failures { get; }
}