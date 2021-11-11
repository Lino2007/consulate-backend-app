using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Collation
{
    public class FilterCriteria
    {
        public string ColumnName { get; set; }
        public string FilterTerm { get; set; }
        public bool IsExactMatch { get; set; }
    }
}
