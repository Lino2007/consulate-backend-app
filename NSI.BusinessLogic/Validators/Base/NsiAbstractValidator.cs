using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BusinessLogic.Validators.Base
{
    public abstract class NsiAbstractValidator<T> : AbstractValidator<T>, INsiValidator<T> where T : class
    {
        public new ValidationResult Validate(T instance)
        {
            ValidationResult preValidationResult = PreValidate(instance);
            if (!preValidationResult.IsValid)
                return preValidationResult;

            var validationResult = base.Validate(instance);

            return PostValidate(instance, preValidationResult, validationResult);
        }

        public virtual ValidationResult PostValidate(T instance, ValidationResult preValidationResult, ValidationResult validationResult)
        {
            return validationResult;
        }

        public virtual ValidationResult PreValidate(T instance)
        {
            return new ValidationResult();
        }
    }
}
