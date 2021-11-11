using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Proxy.Azure.Request
{
    public class GetWorkItemsBatchRequest
    {
        public IList<int> Ids { get; set; }
        public IList<string> Fields { get; set; }
    }
}
