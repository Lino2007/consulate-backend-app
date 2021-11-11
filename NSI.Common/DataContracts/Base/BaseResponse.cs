using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.DataContracts.Base
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public Error Error { get; set; }
        public ResponseStatus Success { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public Severity Severity { get; set; }
    }
}
