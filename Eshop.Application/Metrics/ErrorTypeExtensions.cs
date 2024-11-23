using Eshop.Domain.SeedWork;

namespace Eshop.API.Service;

public static class ErrorTypeExtensions
{
    public static ErrorType FromException(Exception ex)
    {
        return ex switch
        {
            BusinessRuleValidationException => ErrorType.BusinessRuleValidation,
            UnauthorizedAccessException => ErrorType.UnauthorizedAccess,
            ArgumentNullException => ErrorType.ArgumentNull,
            InvalidOperationException => ErrorType.InvalidOperation,
            _ => ErrorType.Unhandled
        };
    }

    public static string ToMetricName(this ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.BusinessRuleValidation => "business_rule_validation",
            ErrorType.UnauthorizedAccess => "unauthorized_access",
            ErrorType.ArgumentNull => "argument_null",
            ErrorType.InvalidOperation => "invalid_operation",
            ErrorType.Unhandled => "unhandled_exception",
            _ => "unknown"
        };
    }
}
