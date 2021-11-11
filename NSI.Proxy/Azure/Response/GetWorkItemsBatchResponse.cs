using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Proxy.Azure.Response
{
    public class GetWorkItemsBatchResponse
    {
        public List<WorkItemBatch> Value { get; set; }
    }

    public class WorkItemBatch
    {
        public string Rev { get; set; }
        public string Url { get; set; }
        public WorkItemDto Fields { get; set; }
    }
}
