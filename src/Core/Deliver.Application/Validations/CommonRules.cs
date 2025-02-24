using FluentValidation;

namespace Deliver.Application.Validations;

public static class CommonRules
{
    public static IRuleBuilderOptions<T, string> ValidName<T>(
        this IRuleBuilder<T, string> ruleBuilder
    )
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Name Is Required")
            .Length(3, 20)
            .WithMessage("Name must be between 3 and 20 characters");
    }
}