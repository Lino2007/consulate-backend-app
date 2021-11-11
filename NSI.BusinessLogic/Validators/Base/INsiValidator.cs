using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BusinessLogic.Validators.Base
{
    public interface INsiValidator<T> where T : class
    {
        ValidationResult PreValidate(T instance);
        ValidationResult PostValidate(T instance, ValidationResult preValidationResult, ValidationResult validationResult);
    }
}
