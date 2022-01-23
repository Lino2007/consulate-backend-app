using NSI.DataContracts.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NSI.Proxy.Azure.Response
{
    [ExcludeFromCodeCoverage]
    public class GetWorkItemsBatchResponse
    {
        public List<WorkItemBatch> Value { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class WorkItemBatch
    {
        public string Rev { get; set; }
        public string Url { get; set; }
        public WorkItemDto Fields { get; set; }
    }
}
