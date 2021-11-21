using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.Common.DataContracts.Base;
using System.Collections.Generic;

namespace NSI.DataContracts.Request
{
    public class BasicRequest : BaseRequest, IFilterable, ISortable, IPageable
    {
        public IList<FilterCriteria> FilterCriteria { get; set; }
        public IList<SortCriteria> SortCriteria { get; set; }
        public Paging Paging { get; set; }
    }
}
