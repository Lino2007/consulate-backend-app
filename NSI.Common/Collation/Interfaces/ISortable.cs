using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Collation.Interfaces
{
    public interface ISortable
    {
        IList<SortCriteria> SortCriteria { get; set; }
    }
}
