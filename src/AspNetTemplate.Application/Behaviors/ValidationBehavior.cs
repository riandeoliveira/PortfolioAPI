using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace AspNetTemplate.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default
    )
    {
        ValidationContext<TRequest> context = new(request);

        IEnumerable<Task<ValidationResult>> validationTasks = validators.Select(
            validator => validator.ValidateAsync(context, cancellationToken)
        );

        ValidationResult[] validationResults = await Task.WhenAll(validationTasks);

        string? error = validationResults
            .SelectMany(result => result.Errors)
            .Select(failure => failure.ErrorMessage)
            .FirstOrDefault(errorMessage => errorMessage != null);

        return error is not null ? throw new ValidationException(error) : await next();
    }
}
