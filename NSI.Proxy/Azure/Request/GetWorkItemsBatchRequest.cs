using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NSI.Proxy.Azure.Request
{
    [ExcludeFromCodeCoverage]
    public class GetWorkItemsBatchRequest
    {
        public IList<int> Ids { get; set; }
        public IList<string> Fields { get; set; }
    }
}
