using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = Deliver.Application.Exceptions.ValidationException;

namespace Deliver.Application.Validations;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();
        var validationResult = new ValidationResult(failures);

        if (failures.Any()) throw new ValidationException(validationResult);

        return await next();
    }
}