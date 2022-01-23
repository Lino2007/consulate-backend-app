using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.Common.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NSI.DataContracts.Request
{
    [ExcludeFromCodeCoverage]
    public class SearchWorkItemsRequest : BaseRequest, IFilterable, ISortable, IPageable
    {
        public IList<int> WorkItemIds { get; set; }
        public IList<FilterCriteria> FilterCriteria { get; set; }
        public IList<SortCriteria> SortCriteria { get; set; }
        public Paging Paging { get; set; }
    }
}
