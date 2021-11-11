using NSI.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSI.Common.Extensions
{
    public static class IOrderedQueryableExtension
    {
        #region IOrderedQueryable Extension Methods

        /// <summary>
        /// Orders IOrderedQueryable Ascending
        /// </summary>
        /// <typeparam name="T">IOrderedQueryable</typeparam>
        /// <param name="source">Source IQueryable to OrderByAscending</param>
        /// <param name="property">Property for ordering</param>
        /// <returns><see cref="IOrderedQueryable<typeparamref name="T"/>"/></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return SortingHelper.ApplyOrder<T>(source, property, "OrderBy");
        }

        /// <summary>
        /// Orders IOrderedQueryable Descending
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueryable to OrderByDescending</param>
        /// <param name="property">Property for ordering</param>
        /// <returns><see cref="IOrderedQueryable<typeparamref name="T"/>"/></returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return SortingHelper.ApplyOrder<T>(source, property, "OrderByDescending");
        }

        /// <summary>
        /// Orders IOrderedQueryable ThenByAscending
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueryable to order ThenByAscending</param>
        /// <param name="property">Property for ordering</param>
        /// <returns><see cref="IOrderedQueryable<typeparamref name="T"/>"/></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return SortingHelper.ApplyOrder<T>(source, property, "ThenBy");
        }

        /// <summary>
        /// Orders IOrderedQueryable ThenByDescending
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueryable to order ThenByDescending</param>
        /// <param name="property">Property for ordering</param>
        /// <returns><see cref="IOrderedQueryable<typeparamref name="T"/>"/></returns>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return SortingHelper.ApplyOrder<T>(source, property, "ThenByDescending");
        }

        #endregion
    }
}
