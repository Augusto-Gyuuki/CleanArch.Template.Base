using FluentValidation;

namespace CleanArch.Base.Template.Application.Common.Validators;

public sealed class Validator<T> where T : class
{
    private readonly IValidator<T>? _validator;

    public Validator(IValidator<T>? validator = null)
    {
        _validator = validator;
    }

    public async Task ValidateAsync(T obj)
    {
        if (_validator is null)
        {
            return;
        }

        await _validator.ValidateAndThrowAsync(obj);
    }
}
