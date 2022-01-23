using NSI.Common.Collation;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NSI.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IQueryableExtension
    {

        #region IQueryable Extension Methods

        #region Paging methods
        /// <summary>
        /// Pages the source according to Paging details provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueriable for which Paging should be added</param>
        /// <param name="paging">Paging contract. Must have RecordsPerPage and Page.</param>
        /// <returns><see cref="IQueryable<typeparamref name="T"/>"/></returns>
        public static IQueryable<T> DoPaging<T>(this IQueryable<T> source, Paging paging)
        {
            if (paging == null)
            {
                return source;
            }

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            int skipElements, takeElements;
            PagingHelper.CalculatePagingDetails(paging, source.Count(), out skipElements, out takeElements);

            return source is IOrderedQueryable ?
                source.Skip(skipElements).Take(takeElements) :
                source.OrderBy(p => 0).Skip(skipElements).Take(takeElements);
        }

        /// <summary>
        /// Pages the source according to Paging details provided asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueriable for which Paging should be added</param>
        /// <param name="paging">Paging contract. Must have RecordsPerPage and Page.</param>
        /// <returns><see cref="IQueryable<typeparamref name="T"/>"/></returns>
        public static async Task<IQueryable<T>> DoPagingAsync<T>(this IQueryable<T> source, Paging paging)
        {
            if (paging == null)
            {
                return source;
            }

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            int skipElements, takeElements;
            PagingHelper.CalculatePagingDetails(paging, await source.CountAsync(), out skipElements, out takeElements);

            return source is IOrderedQueryable ?
                source.Skip(skipElements).Take(takeElements) :
                source.OrderBy(p => 0).Skip(skipElements).Take(takeElements);
        }
        #endregion

        #region Sorting methods

        /// <summary>
        /// Sorts IQueryable source according to provided sort criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueryable to sort</param>
        /// <param name="sortCriteria">IEnumerable of SortCriterias</param>
        /// <returns><see cref="IOrderedQueryable<typeparamref name="T"/>"/></returns>
        public static IOrderedQueryable<T> DoSorting<T>(this IQueryable<T> source, IEnumerable<SortCriteria> sortCriteria)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            IOrderedQueryable<T> ordered = source.OrderBy(p => 0);

            if (!ordered.Any() || sortCriteria == null)
            {
                return ordered;
            }

            int index = 0;

            using (var sequenceEnum = sortCriteria.OrderBy(x => x.Priority).GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    if (index == 0)
                    {
                        ordered = (sequenceEnum.Current.Order == SortOrder.Ascending) ? 
                            source.OrderBy(sequenceEnum.Current.Column) : 
                            source.OrderByDescending(sequenceEnum.Current.Column);
                    }
                    else
                    {
                        ordered = (sequenceEnum.Current.Order == SortOrder.Ascending) ? 
                            ordered.ThenBy(sequenceEnum.Current.Column) : 
                            ordered.ThenByDescending(sequenceEnum.Current.Column);
                    }
                    index++;
                }
            }

            return ordered;
        }

        /// <summary>
        /// Sorts IQueryable source according to provided sort criteria  and sort functor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueryable</param>
        /// <param name="sortCriteria">SortCriteria to be used for sorting</param>
        /// <param name="sortFunctor">Sort functor to be used for sorting</param>
        /// <returns><see cref="IOrderedQueryable<typeparamref name="T"/>"/></returns>
        public static IOrderedQueryable<T> DoSorting<T>(this IQueryable<T> source, IEnumerable<SortCriteria> sortCriteria, Func<string, Expression<Func<T, object>>> sortFunctor)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            // Convert to IOrderedQueryable, will leave current order
            var ordered = source.OrderBy(x => 0);

            if (!ordered.Any() || sortCriteria == null || !sortCriteria.Any())
            {
                return ordered;
            }

            foreach (var sortRule in sortCriteria.OrderBy(x => x.Priority))
            {
                ordered = SortingHelper.ApplySortRule(ordered, sortRule, sortFunctor);
            }

            return ordered;
        }

        #endregion

        #region Filter methods

        /// <summary>
        /// Filters IQueryable source according to provided filter criteria  and filter functor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source IQueryable</param>
        /// <param name="filterCriteria">FilterCriteria to be used for filtering</param>
        /// <param name="filterFunctor">Filter functor to be used for filtering</param>
        /// <returns><see cref="IQueryable<typeparamref name="T"/>"/></returns>
        public static IQueryable<T> DoFiltering<T>(this IQueryable<T> source, IEnumerable<FilterCriteria> filterCriteria, Func<string, string, bool, Expression<Func<T, bool>>> filterFunctor)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (!source.Any() || filterCriteria == null || !filterCriteria.Any())
            {
                return source;
            }

            foreach (var filterRule in filterCriteria)
            {
                source = FilterHelper.ApplyFilterRule(source, filterRule, filterFunctor);
            }

            return source;
        }

        #endregion

        #endregion
    }
}
