using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Collation.Interfaces
{
    public interface IFilterable
    {
        IList<FilterCriteria> FilterCriteria { get; set; }
    }
}
