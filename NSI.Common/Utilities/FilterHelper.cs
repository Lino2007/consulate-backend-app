using NSI.Common.Collation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace NSI.Common.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class FilterHelper
    {
        public static IQueryable<T> ApplyFilterRule<T>(IQueryable<T> data, FilterCriteria rule, Func<string, string, bool, Expression<Func<T, bool>>> filterFunctor)
        {
            if (rule == null || string.IsNullOrWhiteSpace(rule.ColumnName))
            {
                return data;
            }

            Expression<Func<T, bool>> fnc = filterFunctor(rule.ColumnName, rule.FilterTerm, rule.IsExactMatch);

            if (fnc == null)
            {
                throw new ArgumentException("fnc");
            }

            return data.Where(fnc);
        }
    }
}
