using NSI.Common.DataContracts.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace NSI.Common.Collation
{
    [ExcludeFromCodeCoverage]
    public class SortCriteria
    {
        public string Column { get; set; }
        public SortOrder Order { get; set; }
        public int Priority { get; set; }
    }
}
