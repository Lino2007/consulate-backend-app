using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ValidationHelper
    {
        public static Common.DataContracts.Base.Error ToErrorResponse(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelStateDictionary)
        {
            var errorResponse = new Common.DataContracts.Base.Error();

            if (modelStateDictionary.IsValid)
                return errorResponse;

            errorResponse.Severity = Common.Enumerations.Severity.Error;
            errorResponse.Errors = modelStateDictionary
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

            return errorResponse;
        }
    }
}
