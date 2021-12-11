using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DataContracts.Request
{
    public class ReqItemRequest : BasicRequest
    {
        public String id { get; set; }
        public RequestState RequestState { get; set; }
    }
}
