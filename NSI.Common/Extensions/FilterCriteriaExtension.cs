using NSI.Common.Collation;
using NSI.Common.Exceptions;
using NSI.Common.Resources;
using System.Diagnostics.CodeAnalysis;

namespace NSI.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class FilterCriteriaExtension
    {
        /// <summary>
        /// Performs sanity validation of filter criteria. Does not validate if filter criteria object is null.
        /// </summary>
        /// <param name="filterCriteria"><see cref="FilterCriteria"/></param>
        public static void ValidateFilterCriteria(this FilterCriteria filterCriteria)
        {
            if (filterCriteria != null && string.IsNullOrWhiteSpace(filterCriteria.ColumnName))
            {
                throw new NsiArgumentException(ExceptionMessages.FilterColumnNameEmpty);
            }
        }
    }
}
