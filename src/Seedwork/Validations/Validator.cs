using System.Linq;
using FluentValidation;
using Hqv.Seedwork.Exceptions;

namespace Hqv.Seedwork.Validations
{
    public static class Validator
    {
        public static void Validate<TT, TValidator>(TT obj)
            where TValidator : AbstractValidator<TT>, new()
        {
            var validator = new TValidator();
            var validationResult = validator.Validate(obj);
            if (validationResult.IsValid) return;

            var message = string.Join(".  ", validationResult.Errors.Select(x => x.ErrorMessage));
            var exception = new HqvException($"Validation failed. Detail is {message}");           
            exception.Data["errors"] = validationResult.Errors;
            throw exception;
        }

        public static void Validate<TT, TValidator>(TT obj, TValidator validator)
            where TValidator : AbstractValidator<TT>, new()
        {            
            var validationResult = validator.Validate(obj);
            if (validationResult.IsValid) return;

            var message = string.Join(".  ", validationResult.Errors.Select(x => x.ErrorMessage));
            var exception = new HqvException($"Validation failed. Detail is {message}");
            exception.Data["errors"] = validationResult.Errors;
            throw exception;
        }
    }
}
