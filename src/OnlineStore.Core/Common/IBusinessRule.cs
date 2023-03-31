namespace OnlineStore.Core.Common;

public interface IBusinessRule
{
    RuleResult Check();
}