using NSI.Common.Collation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.DataContracts.Base
{
    public abstract class BaseRequest
    {

    }

    public abstract class DataRequest<T> : BaseRequest
    {
        public T FilterCriteria { get; set; }
        public ICollection<SortCriteria> SortCriteria { get; set; }
        public Paging Paging { get; set; }
    }
}
