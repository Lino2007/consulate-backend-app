using NSI.Common.DataContracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Collation
{
    public class SortCriteria
    {
        public string Column { get; set; }
        public SortOrder Order { get; set; }
        public int Priority { get; set; }
    }
}
