using Daily.Service.Exceptions;
using FluentValidation;

namespace Daily.Service.Extensions;

public static class ValidatorExtension
{
    public static async Task EnsureValidatedAsync<TModel>(this IValidator<TModel> validator, TModel model)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
            throw new ArgumentIsNotValidException(validationResult.Errors?.FirstOrDefault()?.ErrorMessage);
    }
}
