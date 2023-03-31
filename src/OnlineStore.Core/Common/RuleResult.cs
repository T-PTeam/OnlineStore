namespace OnlineStore.Core.Common;

public class RuleResult
{
    private RuleResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Errors = new List<string>().AsReadOnly();
    }

    private RuleResult(bool isSuccess, IEnumerable<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors.ToList().AsReadOnly();
    }

    public bool IsSuccess { get; }

    public bool IsFailed => !IsSuccess;

    public IReadOnlyCollection<string> Errors { get; }

    public static RuleResult Success()
    {
        return new RuleResult(true);
    }

    public static RuleResult Failed(params string[] errors)
    {
        return new RuleResult(false, errors);
    }

    public static RuleResult Failed(List<string> errors)
    {
        return new RuleResult(false, errors);
    }

    public static RuleResult Determine(List<string> errors)
    {
        var isSuccess = errors.All(string.IsNullOrWhiteSpace);
        errors = isSuccess ? new List<string>() : errors;
        return new RuleResult(isSuccess, errors);
    }

    public static RuleResult Determine(params string[] errors)
    {
        var isSuccess = errors.All(string.IsNullOrWhiteSpace);
        errors = isSuccess ? Array.Empty<string>() : errors;
        return new RuleResult(isSuccess, errors);
    }
}
